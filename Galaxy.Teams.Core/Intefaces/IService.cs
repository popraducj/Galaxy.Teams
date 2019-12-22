using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Galaxy.Teams.Core.Models;

namespace Galaxy.Teams.Core.Intefaces
{
    public interface IService<T>
    {
        Task<ActionResponse> AddAsync(T model);
        Task<ActionResponse> UpdateAsync(T model);
        Task<List<T>> GetAllAsync();
        T GetById(Guid id);
    }
}