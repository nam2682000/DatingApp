using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
        using Application.DTOs.Responses;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities.Entity;
using MongoDB.Driver;

namespace Application.Service
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public MessageService(IMessageRepository messageRepository, IUserRepository userRepository, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public Task<List<MessageAllResponse>> GetAllMessageForUsers(string userId)
        {
            throw new NotImplementedException();
        }

         public async Task<MessageResponses> GetMessageByUser(string userId, string receiverId)
        {
            var result = new MessageResponses();
            var userIdBson = new MongoDB.Bson.ObjectId(userId);
            var receiverIdBson = new MongoDB.Bson.ObjectId(receiverId);
            var filter = Builders<Message>.Filter.Where(m=>(m.SenderId == userIdBson && m.ReceiverId == receiverIdBson) || (m.SenderId == receiverIdBson && m.ReceiverId == userIdBson));
            var mess = await _messageRepository.GetWhereSelectAsync(filter);
            var receiver = await _userRepository.FindByIdAsync(receiverId);
            result.Messages = mess;
            result.User = _mapper.Map<UserMessageResponse>(receiver);
            return result;
        }

        public async Task<bool> SendMessage(string userFromId, string userToId, string mess)
        {
            var userFrom = new MongoDB.Bson.ObjectId(userFromId);
            var userTo = new MongoDB.Bson.ObjectId(userFromId);
            await _messageRepository.CreateAsync(new Message{
                SenderId = userFrom,
                ReceiverId = userTo,
                Content = mess,
                MessageAt = DateTime.Now
            });
            return true;
        }
    }
}