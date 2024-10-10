using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.Requests;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.Services
{
    public interface ISignalRService
    {
        Task SendMessageToUser(string userId, string content);
    }
}