using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Domain.Entities.Entity;
public class Interest
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string InterestName  { get; set; }      
}