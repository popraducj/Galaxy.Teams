using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Galaxy.Teams.Core.Models;

namespace Galaxy.Teams.Core.Intefaces
{
    public interface ICaptainService
    {
        Task<ActionResponse> AddAsync(Captain captain);
        Task<ActionResponse> UpdateAsync(Captain captain);
        Task<List<Captain>> GetAllAsync();
        Captain GetById(Guid id);
    }
}