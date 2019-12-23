using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Galaxy.Teams.Core.Enums;
using Galaxy.Teams.Core.Helpers;
using Galaxy.Teams.Core.Intefaces;
using Galaxy.Teams.Core.Models;

namespace Galaxy.Teams.Core.Services
{
    public class RobotService : IService<Robot>
    {
        private readonly IRepository<Robot> _repository;

        public RobotService(IRepository<Robot> repository)
        {
            _repository = repository;
        }
        public async Task<ActionResponse> AddAsync(Robot model)
        {
            model.UpdatedAt = model.CreatedAt = DateTime.UtcNow;
            return await _repository.AddAsync(model);
        }

        public async Task<ActionResponse> UpdateAsync(Robot model)
        {
            model.UpdatedAt = DateTime.UtcNow;
            return await _repository.UpdateAsync(model);
        }

        public async Task<ActionResponse> UpdateStatusAsync(Robot model)
        {
            var robot = _repository.GetById(model.Id);
            robot.Status = model.Status;
            return await _repository.UpdateAsync(model);
        }

        public async Task<List<Robot>> GetAllAsync()
        { 
            var robots = await _repository.GetAsync(x=> x.Status != RobotStatus.Deleted, x => x.OrderByDescending(y => y.UpdatedAt));
            return robots.ToList();
        }

        public Robot GetById(Guid id)
        {
            return _repository.GetById(id);
        }
    }
}