using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Setting;
using Domain.Entities.Entity;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Context
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        public MongoDbContext(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var client = new MongoClient(mongoDbSettings.Value.MongoDB);
            _database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
        }
        public IMongoDatabase Database => _database;
        public IMongoCollection<User> Users => _database.GetCollection<User>("User");
        public IMongoCollection<Role> Roles => _database.GetCollection<Role>("Role");
        public IMongoCollection<Like> Likes => _database.GetCollection<Like>("Like");
        public IMongoCollection<Match> Matchs => _database.GetCollection<Match>("Match");
        public IMongoCollection<Block> Blocks => _database.GetCollection<Block>("Block");
        public IMongoCollection<Message> Messages => _database.GetCollection<Message>("Message");
        public IMongoCollection<Interest> Interests => _database.GetCollection<Interest>("Interest");
        public IMongoCollection<ViewUser> ViewUsers => _database.GetCollection<ViewUser>("ViewerUser");
    }
}