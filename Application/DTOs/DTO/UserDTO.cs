using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Entity;
using MongoDB.Bson;
using MongoDB.Driver.GeoJsonObjectModel;

namespace Application.DTOs.DTO
{
    public class UserDTO
    {
        public ObjectId Id { get; set; }
        public required string Username { get; set; }
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public required Role Role { get; set; }
        public required DateTime DateOfBirth { get; set; }
        public required string Gender { get; set; }
        public GeoJsonPoint<GeoJson2DCoordinates>? Location { get; set; }
        public bool EmailIsActive { get; set; }
        public string? ProfilePicture { get; set; } // Đường dẫn đến ảnh hồ sơ
        public string? Bio { get; set; } // Mô tả cá nhân
        public List<Interest>? Interests { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public DateTime LastActive { get; set; }
        public ObjectId RoleId { get; set; }
        public List<ObjectId>? InterestIds { get; set; }
        public string? Status { get; set; } // Mô tả cá nhân
    }
}