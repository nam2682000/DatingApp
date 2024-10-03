using System.Security.Claims;
using Application.DTOs.Requests;
using Application.DTOs.Requests.User;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private IUserService _userService;
    public UserController(ILogger<UserController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var userId = User.FindFirstValue("userId");
        if (userId is not null)
        {
            var data = await _userService.GetNewUser(userId);
            return Ok(data);
        }
        return BadRequest();
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetAll()
    { 
        var data = await _userService.GetAll();
        return Ok(data);
    }

    [HttpPut("user-profile")]
    public async Task<IActionResult> UserUpdateProfile(UserProfileRequest model)
    {
        var userId = User.FindFirstValue("userId");
        if (userId is not null)
        {
            var data = await _userService.UserUpdateProfile(userId, model);
            return Ok(data);
        }
        return BadRequest();
    }

    [HttpPost("user-register")]
    public async Task<IActionResult> UserUpdateProfile(UserRegisterRequest model)
    {
        var data = await _userService.UserRegister(model);
        return Ok(data);
    }
}
