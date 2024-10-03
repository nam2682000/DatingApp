using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities.Entity;
public class Like
{
    public ObjectId Id { get; set; }
    public ObjectId UserId { get; set; } // Người dùng thích
    public ObjectId UserLikeeId { get; set; } // Người dùng được thích
    public DateTime LikedAt { get; set; }
}