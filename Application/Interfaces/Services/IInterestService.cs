using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.Requests;
using Domain.Entities.Entity;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.Services
{
    public interface IInterestService
    {
        Task<IEnumerable<Interest>> GetAllAsync();
    }
}