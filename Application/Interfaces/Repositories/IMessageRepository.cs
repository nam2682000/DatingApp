using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Entity;

namespace Application.Interfaces.Repositories
{
    public interface IMessageRepository:IRepositoryBase<Message>
    {
    }
}