using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Application.Security
{
    public static class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            // Tạo salt ngẫu nhiên
            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            // Sử dụng PBKDF2 để tạo hash
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            // Kết hợp salt và hash thành một chuỗi
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            // Trả về chuỗi mã hóa Base64
            return Convert.ToBase64String(hashBytes);
        }

        public static bool VerifyPassword(string storedPassword, string enteredPassword)
        {
            // Lấy chuỗi salt từ mật khẩu đã lưu
            byte[] hashBytes = Convert.FromBase64String(storedPassword);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            // Tạo hash từ mật khẩu nhập
            var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            // So sánh hash từ mật khẩu nhập và hash đã lưu
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}