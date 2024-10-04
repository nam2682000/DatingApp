using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver.GeoJsonObjectModel;

namespace Application.DTOs.Requests.User
{
    public class UserProfileRequest
    {
        public required string Username { get; set; }
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public required string Email { get; set; }
        public required DateTime DateOfBirth { get; set; }
        public required string Gender { get; set; }
        public GeoJsonPoint<GeoJson2DCoordinates>? Location { get; set; }
        public string? ProfilePicture { get; set; } // Đường dẫn đến ảnh hồ sơ
        public string? Bio { get; set; } // Mô tả cá nhân
        public List<string>? Interests { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}