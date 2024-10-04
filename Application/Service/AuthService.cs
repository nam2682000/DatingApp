using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Security;
using Domain.Entities.Entity;
using MongoDB.Driver;

namespace Application.Service
{
    public class AuthService : IAuthService
    {
        private readonly IJwtTokenService _jwtToken;
        private readonly IUserRepository _userRepository;
        public AuthService(IJwtTokenService jwtToken, IUserRepository userRepository)
        {
            _jwtToken = jwtToken;
            _userRepository = userRepository;
        }

        public async Task<string> Login(string userName, string pass, bool rememberMe)
        {
            string token = string.Empty;
            var filter = Builders<User>.Filter.Where(m=>m.Username == userName);
            var user = await _userRepository.GetUserWithReferenceAsync(filter);
            if(user is null){
                throw new Exception("User name not found");
            }
            bool checkPass = PasswordHasher.VerifyPassword(user.PasswordHash,pass);
            if(checkPass){
                token = _jwtToken.GenerateToken(user.Username,user.Id.ToString(), user.Role.RoleName,rememberMe);
            }
            return token;
        }
    }
}