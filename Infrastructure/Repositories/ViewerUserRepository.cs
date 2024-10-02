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
    public class ViewUserRepository : RepositoryBase<ViewUser>, IViewerUserRepository
    {
        private readonly MongoDbContext _context;
        public ViewUserRepository(IMongoDatabase database, MongoDbContext context) : base(database, "ViewerUser")
        {
            _context = context;
        }
    }
}