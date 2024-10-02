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
    public class InterestRepository : RepositoryBase<Interest>, IInterestRepository
    {
        private readonly MongoDbContext _context;
        public InterestRepository(IMongoDatabase database, MongoDbContext context) : base(database, "Interest")
        {
            _context = context;
        }
    }
}