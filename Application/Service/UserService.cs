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
        private readonly IViewerUserRepository _viewerUserRepository;
        private readonly ILikeRepository _likeRepository;
        private readonly IMapper _mapper;

        public UserService(
            IUserRepository userRepository, IRoleRepository roleRepository,
            IMapper mapper, IViewerUserRepository viewerUserRepository, ILikeRepository likeRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
            _viewerUserRepository = viewerUserRepository;
            _likeRepository = likeRepository;
        }
        public async Task<bool> UserRegister(UserRegisterRequest model)
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

        public async Task<List<UserProfileReponse>> GetAll()
        {
            var filter = Builders<User>.Filter.Empty;
            var users = await _userRepository.GetUserAllWithReferenceAsync(filter);
            var result = _mapper.Map<List<UserProfileReponse>>(users);
            return result;
        }

        public async Task<UserProfileReponse> GetNewUser(string userId)
        {
            var userIdBson = new MongoDB.Bson.ObjectId(userId);
            var fields = Builders<ViewUser>.Projection.Include(p => p.UserViewedId);
            var filter = Builders<ViewUser>.Filter.Where(m => m.UserId == userIdBson);
            var userVieweds = await _viewerUserRepository.GetWhereSelectAsync(filter, fields);
            var userViewedIds = userVieweds.Select(m => m.UserViewedId).ToArray();
            var filterUser = Builders<User>.Filter.Where(m => !userViewedIds.Contains(m.Id));
            var userNew = await _userRepository.FindWhereAsync(filterUser);
            return _mapper.Map<UserProfileReponse>(userNew);
        }

        public async Task<bool> UserLikeUser(string userLikerId, string userLikeeId)
        {
            return await _userRepository.UserLikeUser(userLikerId, userLikeeId);
        }

        public async Task<bool> UserNextUser(string userId, string userNextedId)
        {
            await _viewerUserRepository.CreateAsync(new ViewUser
            {
                UserId = new MongoDB.Bson.ObjectId(userId),
                UserViewedId = new MongoDB.Bson.ObjectId(userNextedId),
            });
            return true;
        }

        public async Task<bool> UserUpdateProfile(string userId, UserProfileRequest model)
        {
            var user = await _userRepository.FindByIdAsync(userId);
            user.Username = model.Username;
            user.Firstname = model.Firstname;
            user.Lastname = model.Lastname;
            user.Email = model.Email;
            user.Latitude = model.Latitude;
            user.Longitude = model.Longitude;
            user.DateOfBirth = model.DateOfBirth;
            user.Gender = model.Gender;
            user.InterestIds = model.Interests?.Select(m => new MongoDB.Bson.ObjectId(m)).ToList();
            return await _userRepository.UpdateAsync(userId, user);
        }

        /// <summary>
        /// Check user 2 is like user 1
        /// </summary>
        /// <param name="user1"></param>
        /// <param name="user2"></param>
        /// <returns></returns>
        private async Task<bool> CheckIsMatch(MongoDB.Bson.ObjectId user2, MongoDB.Bson.ObjectId user1)
        {
            var filter = Builders<Like>.Filter.Where(m => m.UserId == user2 && m.UserLikeeId == user1);
            var result = await _likeRepository.AnyAsync(filter);
            return result;
        }
    }
}