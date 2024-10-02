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
    public class MessageRepository : RepositoryBase<Message>, IMessageRepository
    {
        private readonly MongoDbContext _context;
        public MessageRepository(IMongoDatabase database, MongoDbContext context) : base(database, "Message")
        {
            _context = context;
        }
    }
}