using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Domain.Entities.Entity;
public class User
{
    public ObjectId Id { get; set; }
    public string Username { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string ProfilePicture { get; set; } // Đường dẫn đến ảnh hồ sơ
    public string Bio { get; set; } // Mô tả cá nhân
    public List<Interest> Interests { get; set; } // Sở thích
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTime CreatedAt { get; set; } 
}