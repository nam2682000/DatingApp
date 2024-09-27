using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities.Entity;
public class Block
{
    public ObjectId Id { get; set; }
    public ObjectId UserId { get; set; } // Người dùng thích
    public ObjectId LikedUserId { get; set; } // Người dùng được thích
    public DateTime Timestamp { get; set; }
}