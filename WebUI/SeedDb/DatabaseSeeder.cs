using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Constants;
using Application.Interfaces.Repositories;
using Application.Security;
using Domain.Entities.Entity;
using MongoDB.Driver;

namespace WebUI.SeedDb
{
    public class DatabaseSeeder
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IInterestRepository _interestRepository;

        public DatabaseSeeder(IRoleRepository roleRepository, IUserRepository userRepository, IInterestRepository interestRepository)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _interestRepository = interestRepository;
        }
        public async Task SeedAsync()
        {
            var fillter = Builders<User>.Filter.Empty;
            var user = await _userRepository.FindWhereAsync(fillter);
            if(user is null){
                var roles = new List<Role>{
                    new Role{
                        RoleName = RoleConstants.User
                    },
                    new Role{
                        RoleName = RoleConstants.UserVip
                    },
                    new Role{
                        RoleName = RoleConstants.Admin
                    }
                };
                await _roleRepository.CreateManyAsync(roles);
                var filter = Builders<Role>.Filter.Empty;
                var rolesInData = await _roleRepository.GetWhereSelectAsync(filter);
                var users = new List<User>
                {
                    new User
                    {
                        Username = "admin",
                        Firstname = "admin",
                        Lastname = "admin",
                        Email = "nam2682000@gmail.com",
                        DateOfBirth = DateTime.Now,
                        Gender = GenderConstants.Male,
                        PasswordHash = PasswordHasher.HashPassword("admin123"),
                        RoleId = rolesInData.FirstOrDefault(m=>m.RoleName == RoleConstants.Admin)!.Id,
                        CreatedAt = DateTime.UtcNow,
                    },
                    new User
                    {
                        Username = "user1",
                        Firstname = "user1",
                        Lastname = "user1",
                        Email = "user1@gmail.com",
                        DateOfBirth = DateTime.Now,
                        Gender = GenderConstants.Male,
                        PasswordHash = PasswordHasher.HashPassword("user1123"),
                        RoleId = rolesInData.FirstOrDefault(m=>m.RoleName == RoleConstants.User)!.Id,
                        CreatedAt = DateTime.UtcNow,
                    },
                    new User
                    {
                        Username = "uservip1",
                        Firstname = "uservip1",
                        Lastname = "uservip1",
                        Email = "uservip1@gmail.com",
                        DateOfBirth = DateTime.Now,
                        Gender = GenderConstants.Male,
                        PasswordHash = PasswordHasher.HashPassword("uservip1123"),
                        RoleId = rolesInData.FirstOrDefault(m=>m.RoleName == RoleConstants.UserVip)!.Id,
                        CreatedAt = DateTime.UtcNow,
                    }
                };
                var interests = new List<Interest>(){
                    new Interest{
                        InterestName = "Xem phim",
                    },
                    new Interest{
                        InterestName = "Đọc sách",
                    },
                    new Interest{
                        InterestName = "Nghe nhạc",
                    },
                    new Interest{
                        InterestName = "Chạy",
                    }
                };

                await _interestRepository.CreateManyAsync(interests);
                await _userRepository.CreateManyAsync(users);
            }
        }
    }
}