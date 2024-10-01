using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Domain.Entities.Entity;
public class Role
{
    public ObjectId Id { get; set; }
    public required string RoleName { get; set; }
}