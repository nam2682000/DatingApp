using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.Responses;

namespace Application.Interfaces.Services
{
    public interface IMessageService
    {
        public Task<bool> SendMessage(string userFromId, string userToId, string mess);
        public Task<List<MessageAllResponse>> GetAllMessageForUsers(string userId);
        public Task<MessageResponses> GetMessageByUser(string userId, string receiverId);
    } 
}