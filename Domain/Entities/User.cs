using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver.GeoJsonObjectModel;

namespace Domain.Entities.Entity;
public class User
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string Username { get; set; }
    public required string Firstname { get; set; }
    public required string Lastname { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public required string RoleId { get; set; }
    public required DateTime DateOfBirth { get; set; }
    public required string Gender { get; set; }
    public GeoJsonPoint<GeoJson2DCoordinates>? Location { get; set; }
    public DateTime LastActive { get; set; }
    public bool EmailIsActive { get; set; }
    public string? ProfilePicture { get; set; } // Đường dẫn đến ảnh hồ sơ
    public string? Bio { get; set; } // Mô tả cá nhân
    public List<string>? InterestIds { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public string? Status { get; set; }
}