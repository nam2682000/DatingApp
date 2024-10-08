using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Constants;
using Application.DTOs.Requests;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Security;
using AutoMapper;
using Domain.Entities.Entity;
using MongoDB.Driver;

namespace Application.Service
{
    public class AuthService : IAuthService
    {
        private readonly IJwtTokenService _jwtToken;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        public AuthService(IJwtTokenService jwtToken, IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper)
        {
            _jwtToken = jwtToken;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }


        public async Task<string> Login(LoginRequest model)
        {
            string token = string.Empty;
            var filter = Builders<User>.Filter.Where(m => m.Username == model.UserName);
            var user = await _userRepository.GetUserWithReferenceAsync(filter);
            if (user is null)
            {
                throw new Exception("User name not found");
            }
            bool checkPass = PasswordHasher.VerifyPassword(user.PasswordHash, model.PassWord);
            if (checkPass)
            {
                token = _jwtToken.GenerateToken(user.Username, user.Id.ToString(), user.Role.RoleName, model.RememberMe);
            }
            return token;
        }

        public Task<string> Logout()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Register(UserRegisterRequest model)
        {
            var filter = Builders<Role>.Filter.Eq(m => m.RoleName, RoleConstants.User);
            var role = await _roleRepository.FindWhereAsync(filter);
            var user = _mapper.Map<User>(model);
            if (role is null)
            {
                throw new Exception("Role is null");
            }
            user.RoleId = role.Id;
            await _userRepository.CreateAsync(user);
            return true;
        }
    }
}