using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Domain.Entities.Entity;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(IMongoDatabase database, string collectionName) : base(database, collectionName)
        {
        }
    }
}