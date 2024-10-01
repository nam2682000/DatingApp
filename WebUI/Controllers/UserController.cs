using Application.DTOs.Requests;
using Application.Interfaces.Services;
using Domain.Entities.Entity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

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
        var data = await _userService.GetAll();
        return Ok(data);
    }

    [HttpPost]
    public async Task<IActionResult> Add(UserRegisterRequest model)
    {
        await _userService.AddUser(model);
        return Ok();
    }
}
