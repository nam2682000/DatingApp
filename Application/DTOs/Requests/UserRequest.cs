using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver.GeoJsonObjectModel;

namespace Application.DTOs.Requests.User
{
    public class UserProfileRequest
    {
        public required string Username { get; set; }
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public required string Email { get; set; }
        public required DateTime DateOfBirth { get; set; }
        public required string Gender { get; set; }
        public string? ProfilePicture { get; set; } // Đường dẫn đến ảnh hồ sơ
        public string? Bio { get; set; } // Mô tả cá nhân
        public List<string>? Interests { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }

    public class UserRegisterRequest
    {
        public required string Username { get; set; }
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public required DateTime DateOfBirth { get; set; }
        public required string Gender { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string RePassword { get; set; }
    }

    public class UserLikeeRequest
    {
        public required string UserLikeeId { get; set; }
    }

    public class UserNextRequest
    {
        public required string UserNextId { get; set; }
    }
}