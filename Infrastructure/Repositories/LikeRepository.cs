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
    public class LikeRepository : RepositoryBase<Like>, ILikeRepository
    {
        private readonly MongoDbContext _context;
        public LikeRepository(IMongoDatabase database, MongoDbContext context) : base(database, "Like")
        {
            _context = context;
        }
    }
}