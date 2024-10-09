using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities.Entity;
public class Message
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string SenderId { get; set; }
    public required string ReceiverId { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime MessageAt { get; set; }
}  