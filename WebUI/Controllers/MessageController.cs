using System.Security.Claims;
using Application.DTOs.Requests;
using Application.DTOs.Requests.User;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;

[ApiController]
[Route("[controller]")]
public class MessageController : ControllerBase
{
    private readonly ILogger<MessageController> _logger;
    private IMessageService _messageService;

    public MessageController(IMessageService messageService, ILogger<MessageController> logger)
    {
        _messageService = messageService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetMessageByUser(string receiverId)
    {
        var userId = User.FindFirstValue("userId");
        if (userId is not null)
        {
            var responses = await _messageService.GetMessageByUser(userId,receiverId);
            return Ok(responses);
        }
        return BadRequest();
    }
}
