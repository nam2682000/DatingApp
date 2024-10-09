using System.Security.Claims;
using Application.DTOs.Requests;
using Application.DTOs.Requests.User;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;

[ApiController]
[Route("[controller]")]
public class InterestController : ControllerBase
{
    private readonly ILogger<InterestController> _logger;
    private IInterestService _interestService;

    public InterestController(IInterestService interestService, ILogger<InterestController> logger)
    {
        _interestService = interestService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var data = await _interestService.GetAllAsync();
        return Ok(data);
    }
}
