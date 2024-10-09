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
    public class InterestService : IInterestService
    {
        private readonly IInterestRepository _interestRepository;

        public InterestService(IInterestRepository interestRepository)
        {
            _interestRepository = interestRepository;
        }
        public async Task<IEnumerable<Interest>> GetAllAsync()
        {
            return await _interestRepository.GetAllAsync();
        }
    }
}