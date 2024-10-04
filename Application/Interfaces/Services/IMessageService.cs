using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IMessageService
    {
        public Task<bool> SendMessage(string userFromId, string userToId, string mess);
    } 
}