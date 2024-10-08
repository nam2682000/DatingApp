using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.Requests;

namespace Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<string> Login(LoginRequest model);
        Task<string> Logout();
        Task<bool> Register(UserRegisterRequest model);
    }
}