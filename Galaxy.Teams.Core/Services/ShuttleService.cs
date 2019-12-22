using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Galaxy.Teams.Core.Intefaces;
using Galaxy.Teams.Core.Models;

namespace Galaxy.Teams.Core.Services
{
    public class ShuttleService : IService<Shuttle>
    {
        private readonly IRepository<Shuttle> _repository;

        public ShuttleService(IRepository<Shuttle> repository)
        {
            _repository = repository;
        }
        public async Task<ActionResponse> AddAsync(Shuttle model)
        {
            model.UpdatedAt = model.CreatedAt = DateTime.UtcNow;
            return await _repository.AddAsync(model);
        }

        public async Task<ActionResponse> UpdateAsync(Shuttle model)
        {
            model.UpdatedAt = DateTime.UtcNow;
            return await _repository.UpdateAsync(model);
        }

        public async Task<List<Shuttle>> GetAllAsync()
        { 
            var shuttles = await _repository.GetAsync(x=> x.Status != ShuttleStatus.Deleted, x => x.OrderByDescending(y => y.UpdatedAt));
            return shuttles.ToList();
        }

        public Shuttle GetById(Guid id)
        {
            return _repository.GetById(id);
        }
    }
}