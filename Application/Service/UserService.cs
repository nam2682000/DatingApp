using System;
using Application.Common.Constants;
using Application.DTOs.Requests;
using Application.DTOs.Requests.User;
using Application.DTOs.Responses;
using Application.DTOs.Responses.User;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities.Entity;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;


namespace Application.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IViewerUserRepository _viewerUserRepository;
        private readonly ILikeRepository _likeRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly IMapper _mapper;

        public UserService(
            IUserRepository userRepository, IMapper mapper, IViewerUserRepository viewerUserRepository, ILikeRepository likeRepository, IMatchRepository matchRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _viewerUserRepository = viewerUserRepository;
            _likeRepository = likeRepository;
            _matchRepository = matchRepository;
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
            var fields = Builders<ViewUser>.Projection.Include(p => p.UserViewedId);
            var filter = Builders<ViewUser>.Filter.Where(m => m.UserId == userId);
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
                UserId = userId,
                UserViewedId = userNextedId,
            });
            return true;
        }

        public async Task<bool> UserUpdateProfile(string userId, UserProfileRequest model)
        {
            var location = new GeoJsonPoint<GeoJson2DCoordinates>(new GeoJson2DCoordinates(model.Longitude??0, model.Latitude??0));
            var user = await _userRepository.FindByIdAsync(userId);
            user.Username = model.Username;
            user.Firstname = model.Firstname;
            user.Lastname = model.Lastname;
            user.Email = model.Email;
            user.DateOfBirth = model.DateOfBirth;
            user.Gender = model.Gender;
            user.Bio = model.Bio;
            user.InterestIds = model.Interests;
            user.Location = location;
            return await _userRepository.UpdateAsync(userId, user);
        }

        public async Task<UserProfileReponse> MyProfile(string userId)
        {
            var filter = Builders<User>.Filter.Where(m=>m.Id == userId);
            var users = await _userRepository.GetUserWithReferenceAsync(filter);
            var result = _mapper.Map<UserProfileReponse>(users);
            return result;
        }

        public async Task<List<UserMessageResponse>> GetAllUserMatch(string userId)
        {
            // Tạo bộ lọc để lấy tất cả các bản ghi match có chứa userId
            var matchFilter = Builders<Match>.Filter.Where(m => m.User1 == userId || m.User2 == userId);
            // Lấy danh sách match từ database
            var matches = await _matchRepository.GetWhereSelectAsync(matchFilter);
            // Tạo danh sách userId của các user đã match với user hiện tại
            var matchedUserIds = matches.Select(m => m.User1 == userId ? m.User2 : m.User1).Distinct().ToArray();
            // Tạo bộ lọc cho danh sách user đã match
            var userFilter = Builders<User>.Filter.In(u => u.Id, matchedUserIds);
            // Lấy thông tin user từ danh sách matchedUserIds
            var matchedUsers = await _userRepository.GetWhereSelectAsync(userFilter);
            // Ánh xạ danh sách matchedUsers sang đối tượng UserMessageResponse
            var result = _mapper.Map<List<UserMessageResponse>>(matchedUsers);
            return result;
        }
    }
}