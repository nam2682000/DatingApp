using Application.DTOs.Requests;
using Application.DTOs.Requests.User;
using Application.DTOs.Responses.User;
using Domain.Entities.Entity;

namespace Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<List<UserProfileReponse>> GetAll();
        Task<bool> AddUser(UserRegisterRequest model);
        Task<bool> UserUpdateProfile(string userId,UserProfileRequest model);

    }
}