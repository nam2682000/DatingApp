using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.Responses;
using Domain.Entities.Entity;

namespace Application.Interfaces.Services
{
    public interface IMessageService
    {
        public Task<Message> SendMessage(string userFromId, string userToId, string mess);
        public Task<List<MessageAllResponse>> GetAllMessageForUsers(string userId);
        public Task<MessageResponses> GetMessageByUser(string userId, string receiverId);

    } 
}