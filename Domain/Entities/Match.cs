using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver.GeoJsonObjectModel;

namespace Domain.Entities.Entity;
public class Match
{
    public ObjectId Id { get; set; }
    public ObjectId User1 { get; set; }
    public ObjectId User2 { get; set; }
    public bool IsActive { get; set; }
    public DateTime MatchedAt { get; set; }
}