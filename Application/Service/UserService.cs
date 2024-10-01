using System;
using Application.Common.Constants;
using Application.DTOs.Requests;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities.Entity;
using MongoDB.Driver;


namespace Application.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }
        public async Task<bool> AddUser(UserRegisterRequest model)
        {
            var filter = Builders<Role>.Filter.Eq(m => m.RoleName, RoleConstants.User);
            var role = await _roleRepository.FindWhereAsync(filter);
            var user = _mapper.Map<User>(model);
            user.RoleId = role.Id;
            await _userRepository.CreateAsync(user);
            return true;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var users = await _userRepository.GetAllAsync();
            return users;
        }
    }
}