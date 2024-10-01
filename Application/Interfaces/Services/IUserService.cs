using Application.DTOs.Requests;
using Domain.Entities.Entity;

namespace Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
        Task<bool> AddUser(UserRegisterRequest user);
    }
}