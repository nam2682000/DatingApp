using System;
using Application.Common.Constants;
using Application.DTOs.Requests;
using Application.DTOs.Requests.User;
using Application.DTOs.Responses.User;
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
            if(role is null){
                throw new Exception("Role is null");
            }
            user.RoleId = role.Id;
            await _userRepository.CreateAsync(user);
            return true;
        }

        public async Task<List<UserProfileReponse>> GetAll()
        {
            var filter = Builders<User>.Filter.Empty;
            var users = await _userRepository.GetUserAllWithReferenceAsync(filter);
            var result = _mapper.Map<List<UserProfileReponse>>(users);
            return result;
        }

        public async Task<bool> UserUpdateProfile(string userId,UserProfileRequest model)
        {
            var user = await _userRepository.FindByIdAsync(userId);
            user.Username = model.Username;
            user.Firstname = model.Firstname;
            user.Lastname = model.Lastname;
            user.Email = model.Email;
            user.Latitude = model.Latitude;
            user.Longitude = model.Longitude;
            user.InterestIds = model.Interests?.Select(m=>new MongoDB.Bson.ObjectId(m)).ToList();
            return await _userRepository.UpdateAsync(userId, user);
        }
    }
}