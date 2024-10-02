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
    public class BlockRepository : RepositoryBase<Block>, IBlockRepository
    {
        private readonly MongoDbContext _context;
        public BlockRepository(IMongoDatabase database, MongoDbContext context) : base(database, "Block")
        {
            _context = context;
        }
    }
}