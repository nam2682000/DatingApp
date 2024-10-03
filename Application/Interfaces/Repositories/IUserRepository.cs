using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.DTO;
using Domain.Entities.Entity;
using MongoDB.Driver;

namespace Application.Interfaces.Repositories
{
    public interface IUserRepository:IRepositoryBase<User>
    {
        public Task<User> GetUserByEmailAsync(string email);
        public Task<List<UserDTO>> GetUserAllWithReferenceAsync(FilterDefinition<User> filter);
        public Task<bool> UserLikeUser(string userLikerId, string userLikeeId);
    }
}