using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson; 

namespace Domain.Entities.Entity;
public class Block
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string UserId { get; set; } // Người dùng thích
    public required string UserBlockedId { get; set; } // Người dùng được thích
    public DateTime BlockAt { get; set; }
}