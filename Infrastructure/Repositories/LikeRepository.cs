using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Domain.Entities.Entity;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class LikeRepository : RepositoryBase<Like>, ILikeRepository
    {
        public LikeRepository(IMongoDatabase database, string collectionName) : base(database, collectionName)
        {
        }
    }
}