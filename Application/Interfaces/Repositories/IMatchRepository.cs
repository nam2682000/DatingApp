using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.DTO;
using Domain.Entities.Entity;
using MongoDB.Driver;

namespace Application.Interfaces.Repositories
{
    public interface IMatchRepository:IRepositoryBase<Match>
    {
    }
}