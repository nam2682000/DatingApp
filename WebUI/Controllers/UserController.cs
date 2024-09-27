using Application.Interfaces.Services;
using Application.Service;
using Domain.Entities.Entity;
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

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IActionResult> Get()
    {
        var data = await _userService.GetAll();
        return Ok(data);
    }

    [HttpPost(Name = "GetWeatherForecast")]
    public async Task<IActionResult> Add()
    {
        var user = new User{
            Username = "user1",
            PasswordHash = "sad",
            Firstname = "first",
            Lastname = "user1"        
        };
        await _userService.Add(user);
        return Ok();
    }
}
