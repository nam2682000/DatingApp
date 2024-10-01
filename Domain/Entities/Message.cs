using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities.Entity;
public class Message
{
    public ObjectId Id { get; set; }
    public ObjectId SenderId { get; set; }
    public ObjectId ReceiverId { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
}