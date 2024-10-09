using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities.Entity;
public class Like
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string UserId { get; set; } // Người dùng thích
    public required string  UserLikeeId { get; set; } // Người dùng được thích
    public DateTime LikedAt { get; set; }
}