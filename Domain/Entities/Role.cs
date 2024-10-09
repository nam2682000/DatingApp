using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Domain.Entities.Entity;
public class Role
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string RoleName { get; set; }
}