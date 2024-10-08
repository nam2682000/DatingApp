using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Constants;
using Application.DTOs.Requests;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Security;
using AutoMapper;
using Domain.Entities.Entity;
using MongoDB.Driver;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace Application.Service
{
    public class FileService : IFileService
    {
        private readonly IUserRepository _userRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment, IUserRepository userRepository)
        {
            _webHostEnvironment = webHostEnvironment;
            _userRepository = userRepository;
        }

        public async Task<string> UserUploadAvatarFile(IFormFile file,  string userId)
        {

            var data = _webHostEnvironment.WebRootPath;
            if (file.Length > 0)
            {
                // Lấy đường dẫn tới thư mục wwwroot
                var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

                // Tạo thư mục nếu chưa tồn tại
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                // Tạo đường dẫn cho file upload
                var filePath = Path.Combine(uploadPath, file.FileName);
                using var stream = File.Create(filePath);
                await file.CopyToAsync(stream);
                return filePath;
            }
            return string.Empty;
        }
    }
}