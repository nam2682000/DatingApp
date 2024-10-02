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
    public class MatchRepository : RepositoryBase<Match>, IMatchRepository
    {
        private readonly MongoDbContext _context;
        public MatchRepository(IMongoDatabase database, MongoDbContext context) : base(database, "Match")
        {
            _context = context;
        }
    }
}