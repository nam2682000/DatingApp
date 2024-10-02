using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Domain.Entities.Entity;
using Infrastructure.Context;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        private readonly MongoDbContext _context;
        public RoleRepository(IMongoDatabase database, MongoDbContext context) : base(database, "Role")
        {
            _context = context;
        }
    }
}