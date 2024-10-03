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

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.Find(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task<bool> UserLikeUser(string userLikerId, string userLikeeId)
        {
            using (var session = await _context.Database.Client.StartSessionAsync())
            {
                session.StartTransaction();
                try
                {
                    var userId1 = new MongoDB.Bson.ObjectId(userLikerId);
                    var userId2 = new MongoDB.Bson.ObjectId(userLikeeId);
                    await _context.Likes.InsertOneAsync(session, new Like
                    {
                        UserId = userId1,
                        UserLikeeId = userId2,
                        LikedAt = DateTime.Now
                    });

                    var checkIsLike = await _context.Likes.Find(session, m => m.UserId == userId2 && m.UserLikeeId == userId1).FirstOrDefaultAsync();
                    if (checkIsLike != null)
                    {
                        await _context.Matchs.InsertOneAsync(session, new Match
                        {
                            User1 = userId1,
                            User2 = userId2,
                            MatchedAt = DateTime.Now
                        });
                    }

                    await _context.ViewUsers.InsertOneAsync(session, new ViewUser
                    {
                        UserId = userId1,
                        UserViewedId = userId2,
                    });

                    await session.CommitTransactionAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    await session.AbortTransactionAsync();
                    Console.WriteLine($"Transaction aborted: {ex.Message}");
                    return false;
                }
            }
        }

    }
}