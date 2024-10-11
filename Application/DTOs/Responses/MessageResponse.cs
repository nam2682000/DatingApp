using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Entity;

namespace Application.DTOs.Responses
{
    public class MessageAllResponse
    {
        public required string LastMess { get; set; }
        public bool IsLastMessSelf { get; set; }
        public DateTime DateTime { get; set; }
        public string? UserId { get; set; }
        public UserMessageResponse? User { get; set; }
    }

    public class MessageResponses
    {
        public List<Message>? Messages { get; set; }
        public UserMessageResponse? User { get; set; }
    }

    public class UserMessageResponse
    {
        public required string Id { get; set; }
        public required string Username { get; set; }
        public required string Firstname { get; set; }  
        public required string Lastname { get; set; }
        public required string Gender { get; set; }
        public DateTime? LastActive { get; set; }
        public string? ProfilePicture { get; set; }
    }
}