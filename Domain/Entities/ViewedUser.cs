using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Domain.Entities.Entity;
public class ViewUser
{
    public ObjectId Id { get; set; }
    public ObjectId UserId { get; set; }
    public ObjectId UserViewedId { get; set; }
    public DateTime ViewAt { get; set; }
}