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

    [HttpGet("my-profile")]
    public async Task<IActionResult> MyProfile()
    {
        var userId = User.FindFirstValue("userId");
        if (userId is not null)
        {
            var data = await _userService.MyProfile(userId);
            return Ok(data);
        }
        return BadRequest();
    }

    [HttpGet("get-new-user")]
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

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var data = await _userService.GetAll();
        return Ok(data);
    }

    [HttpGet("user-match")]
    public async Task<IActionResult> GetAllUserMatch()
    {
        var userId = User.FindFirstValue("userId");
        if (userId is not null)
        {
            var data = await _userService.GetAllUserMatch(userId);
            return Ok(data);
        }
        return BadRequest();
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

    [HttpPost("user-like")]
    public async Task<IActionResult> UserLikeUser([FromBody] UserLikeeRequest model)
    {
        try
        {
            var userId = User.FindFirstValue("userId");
            if (userId is not null)
            {
                var data = await _userService.UserLikeUser(userId, model.UserLikeeId);
                return Ok(data);
            }
        }
        catch (Exception e)
        {
            string mess = e.Message;
        }
        return BadRequest();
    }
    [HttpPost("user-next")]
    public async Task<IActionResult> UserNextUser([FromBody] UserNextRequest model)
    {
        var userId = User.FindFirstValue("userId");
        if (userId is not null)
        {
            var data = await _userService.UserNextUser(userId, model.UserNextId);
            return Ok(data);
        }
        return BadRequest();
    }
}
