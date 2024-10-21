using System.Security.Claims;
using Application.DTOs.Requests;
using Application.DTOs.Requests.User;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;

[ApiController]
[Route("[controller]")]
public class FileController : ControllerBase
{
    private readonly ILogger<FileController> _logger;
    private IFileService _fileService;

    public FileController(IFileService fileService, ILogger<FileController> logger)
    {
        _fileService = fileService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        var userId = User.FindFirstValue("userId");
        if (userId is not null)
        {
            string filePath = await _fileService.UserUploadAvatarFile(file, userId);
            return Ok(filePath);
        }
        return BadRequest();
    }
}
