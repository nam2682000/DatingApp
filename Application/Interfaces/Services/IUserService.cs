using Application.DTOs.Requests.User;
using Application.DTOs.Responses;
using Application.DTOs.Responses.User;


namespace Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<List<UserProfileReponse>> GetAll();
        Task<UserProfileReponse> MyProfile(string userId);
        Task<bool> UserUpdateProfile(string userId, UserProfileRequest model);
        Task<bool> UserLikeUser(string userLikerId, string userLikeeId);
        Task<bool> UserNextUser(string userId, string userNextedId);
        Task<UserProfileReponse> GetNewUser(string userId);
        Task<List<UserMessageResponse>> GetAllUserMatch(string userId);
    }
}