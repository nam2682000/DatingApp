using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Application.DTOs.Requests
{
    public class LoginRequest
    {
        public required string UserName { get; set; }
        public required string PassWord { get; set; }
        public bool RememberMe { get; set; }
    }
}