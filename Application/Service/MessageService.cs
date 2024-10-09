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
            var filter = Builders<Message>.Filter.Where(m=>(m.SenderId == userId && m.ReceiverId == receiverId) || (m.SenderId == receiverId && m.ReceiverId == userId));
            var mess = await _messageRepository.GetWhereSelectAsync(filter);
            var receiver = await _userRepository.FindByIdAsync(receiverId);
            result.Messages = mess;
            result.User = _mapper.Map<UserMessageResponse>(receiver);
            return result;
        }

        public async Task<bool> SendMessage(string userFromId, string userToId, string mess)
        {
            await _messageRepository.CreateAsync(new Message{
                SenderId = userFromId,
                ReceiverId = userToId,
                Content = mess,
                MessageAt = DateTime.Now
            });
            return true;
        }
    }
}