using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.Requests;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebUI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;
        public AuthController(ILogger<AuthController> logger, IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Index([FromBody] LoginRequest request)
        {
            string token = await _authService.Login(request);
            return Ok(token);
        }
        [HttpPost("register")]
        public async Task<IActionResult> UserRegister(UserRegisterRequest model)
        {
            var data = await _authService.Register(model);
            return Ok(data);
        }
    }
}