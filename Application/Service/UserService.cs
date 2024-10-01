using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.Requests;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Security;
using AutoMapper;
using Domain.Entities.Entity;


namespace Application.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<bool> AddUser(UserRegisterRequest model)
        {
            var user = _mapper.Map<User>(model);
            user.PasswordHash = PasswordHasher.HashPassword(model.Password);
            _userRepository.CreateAsync
        }

        public Task<IEnumerable<User>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}