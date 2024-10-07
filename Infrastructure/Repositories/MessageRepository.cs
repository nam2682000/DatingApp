using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Domain.Entities.Entity;
using Infrastructure.Context;
using MongoDB.Bson;
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
        public async Task<List<ConversationDto>> GetAllConversationsWithLatestMessageAsync(ObjectId userId)
        {
            var conversations = await _context.Messages.Aggregate()
                // Lọc các tin nhắn liên quan đến người dùng
                .Match(m => m.SenderId == userId || m.ReceiverId == userId)

                // Sắp xếp tin nhắn theo thời gian để lấy tin nhắn mới nhất trong nhóm
                .Sort(new BsonDocument("MessageAt", -1))
                
                // Nhóm các tin nhắn theo đối tác trong cuộc trò chuyện
                .Group(new BsonDocument
                {
                    { "_id", new BsonDocument
                        {
                            { "PartnerId", new BsonDocument
                                {
                                    { "$cond", new BsonArray 
                                        {
                                            new BsonDocument("$eq", new BsonArray { "$SenderId", userId }),
                                            "$ReceiverId",
                                            "$SenderId"
                                        }
                                    }
                                }
                            }
                        }
                    },
                    { "LatestMessage", new BsonDocument
                        {
                            { "$first", "$$ROOT" }
                        }
                    }
                })

                // Lấy danh sách kết quả
                .ToListAsync();

            // Chuyển đổi kết quả sang dạng DTO
            return conversations.Select(c => new ConversationDto
            {
                UserId = c["_id"]["PartnerId"].AsObjectId,
                LatestMessage = BsonSerializer.Deserialize<Message>(c["LatestMessage"].AsBsonDocument)
            }).ToList();
        }
    }
}