using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IJwtTokenService
    {
        string GenerateToken(string username, string userId, string role);
    }
}