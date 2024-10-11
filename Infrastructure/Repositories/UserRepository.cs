using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.DTO;
using Application.Interfaces.Repositories;
using Domain.Entities.Entity;
using Infrastructure.Context;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private readonly MongoDbContext _context;
        public UserRepository(IMongoDatabase database, MongoDbContext context) : base(database, "User")
        {
            _context = context;
        }
        public async Task<List<UserDTO>> GetUserAllWithReferenceAsync(FilterDefinition<User> filter)
        {
            // Sử dụng Aggregation với Lookup để join Role và Interest
            var result = await _context.Users.Aggregate()
            .Match(filter) // Tìm user theo điều kiện filter
            .Lookup<User, Role, UserDTO>(
                _context.Roles,               // Liên kết với collection Roles
                u => u.RoleId,                // RoleId trong User
                r => r.Id,                    // Id trong Role
                u => u.Role                   // Kết quả gán vào UserDTO.Role
            )
            .Unwind<UserDTO, UserDTO>(x => x.Role)
            .As<User>() // Quay về kiểu `User` sau khi `Lookup` đầu tiên
            .Lookup<User, Interest, UserDTO>(
                _context.Interests,           // Liên kết với collection Interests
                u => u.InterestIds,           // InterestIds trong User
                i => i.Id,                    // Id trong Interest
                u => u.Interests              // Kết quả gán vào UserDTO.Interests
            ).ToListAsync();
            return result;
        }

        public async Task<UserDTO> GetUserWithReferenceAsync(FilterDefinition<User> filter)
        {
            // Sử dụng Aggregation với Lookup để join Role và Interest
            var result = await _context.Users.Aggregate()
            .Match(filter) // Tìm user theo điều kiện filter
            .Lookup<User, Role, UserDTO>(
                _context.Roles,               // Liên kết với collection Roles
                u => u.RoleId,                // RoleId trong User
                r => r.Id,                    // Id trong Role
                u => u.Role                   // Kết quả gán vào UserDTO.Role
            )
            .Unwind<UserDTO, UserDTO>(x => x.Role)
            .As<User>() // Quay về kiểu `User` sau khi `Lookup` đầu tiên
            .Lookup<User, Interest, UserDTO>(
                _context.Interests,           // Liên kết với collection Interests
                u => u.InterestIds,           // InterestIds trong User
                i => i.Id,                    // Id trong Interest
                u => u.Interests              // Kết quả gán vào UserDTO.Interests
            ).FirstOrDefaultAsync();
            return result;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.Find(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task<bool> UserLikeUser(string userLikerId, string userLikeeId)
        {
            try
            {
                var like = new Like
                {
                    UserId = userLikerId,
                    UserLikeeId = userLikeeId,
                    LikedAt = DateTime.Now
                };
                
                await _context.Likes.InsertOneAsync(like);
                await _context.ViewUsers.InsertOneAsync(new ViewUser
                {
                    UserId = userLikerId,
                    UserViewedId = userLikeeId,
                });

                var checkIsLike = await _context.Likes.Find(m => m.UserId == userLikeeId && m.UserLikeeId == userLikerId).FirstOrDefaultAsync();
                if (checkIsLike != null)
                {
                    await _context.Matchs.InsertOneAsync(new Match
                    {
                        User1 = userLikerId,
                        User2 = userLikeeId,
                        MatchedAt = DateTime.Now
                    });
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Transaction aborted: {ex.Message}");
                return false;
            }
        }

    }
}