using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Interfaces.Services;
using Application.Service;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Services
{
    public class ChatHub : Hub
    {
        // Tạo dictionary để lưu trữ mapping giữa user và ConnectionId
        private static Dictionary<string, string> userConnections = new Dictionary<string, string>();

        private readonly IMessageService _messageService;

        public ChatHub(IMessageService messageService)
        {
            _messageService = messageService;
        }

        // Lưu connection của user khi kết nối
        public override Task OnConnectedAsync()
        {
            var userId = Context.User?.FindFirstValue("userId");
            if (!string.IsNullOrEmpty(userId) && !userConnections.ContainsKey(userId))
            {
                userConnections[userId] = Context.ConnectionId; // Mapping userId với ConnectionId
            }
            return base.OnConnectedAsync();
        }

        // Xóa connection khi ngắt kết nối
        public override Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.User?.FindFirstValue("userId");
            if(!string.IsNullOrEmpty(userId) ){
                userConnections.Remove(userId); // Xóa mapping khi user ngắt kết nối
            }
            return base.OnDisconnectedAsync(exception);
        }

        // Phương thức để gửi tin nhắn cá nhân
        public async Task SendMessageToUser(string toUserId, string message)
        {
            var userId = Context.User?.FindFirstValue("userId");
            if(!string.IsNullOrEmpty(userId)){
                await _messageService.SendMessage(userId,toUserId,message);
                // nếu user nhận đăng nhập thì mới gửi 
                if (userConnections.TryGetValue(toUserId, out string connectionId))
                {
                    await Clients.Client(connectionId).SendAsync("ReceiveMessage", userId, message);
                }
            }
        }
    }
}