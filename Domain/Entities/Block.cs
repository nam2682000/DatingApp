using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson; 

namespace Domain.Entities.Entity;
public class Block
{
    public ObjectId Id { get; set; }
    public ObjectId UserId { get; set; } // Người dùng thích
    public ObjectId UserBlockedId { get; set; } // Người dùng được thích
    public DateTime BlockAt { get; set; }
}