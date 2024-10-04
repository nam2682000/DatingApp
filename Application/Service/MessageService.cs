using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Security;
using Domain.Entities.Entity;
using MongoDB.Driver;

namespace Application.Service
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public Task<bool> SendMessage(string userFromId, string userToId, string mess)
        {
            throw new NotImplementedException();
        }
    }
}