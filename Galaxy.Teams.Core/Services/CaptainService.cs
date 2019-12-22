using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Galaxy.Teams.Core.Intefaces;
using Galaxy.Teams.Core.Models;

namespace Galaxy.Teams.Core.Services
{
    public class CaptainService : IService<Captain>
    {
        private readonly IRepository<Captain> _repository;
        private readonly IUserGrpcService _userGrpcService;

        public CaptainService(IRepository<Captain> repository, IUserGrpcService userGrpcService)
        {
            _repository = repository;
            _userGrpcService = userGrpcService;
        }

        public async Task<ActionResponse> AddAsync(Captain model)
        {
            var errors = await ValidateCaptain(model);
            if (errors.Any())
            {
                return new ActionResponse
                {
                    Errors = errors,
                    Success = false
                };
            }

            model.UpdatedAt = model.CreatedAt = DateTime.UtcNow;
            return await _repository.AddAsync(model);
        }

        public async Task<ActionResponse> UpdateAsync(Captain model)
        {
            var errors = await ValidateCaptain(model);
            if (errors.Any())
            {
                return new ActionResponse
                {
                    Errors = errors,
                    Success = false
                };
            }
            
            model.UpdatedAt = DateTime.UtcNow;
            return await _repository.UpdateAsync(model);
        }

        public async Task<List<Captain>> GetAllAsync()
        { 
            var captains = await _repository.GetAsync(x=> x.Status != CaptainStatus.Deleted, x => x.OrderByDescending(y => y.UpdatedAt));
            return captains.ToList();
        }

        public Captain GetById(Guid id)
        {
            return _repository.GetById(id);
        }
        
        private async Task<List<ActionError>> ValidateCaptain(Captain captain)
        {
            var errors = new List<ActionError>();
            if (captain.Age < 18)
            {
                errors.Add(new ActionError
                {
                    Code = "InvalidAge",
                    Description = "Captain age should be at least 18"
                });
            }

            var (userId, name) = await _userGrpcService.GetUserAsync(captain.Username);
            if (userId < 0)
            {
                errors.Add(new ActionError
                {
                    Code = "InvalidUser",
                    Description = "The captain should have a valid user"
                });
            }
            else
            {
                captain.UserId = userId;
                captain.Name = name;
            }

            var sameUser = _repository.GetAsync(x => x.UserId == userId);
            if (sameUser != null)
            {
                errors.Add(new ActionError
                {
                    Code = "SameUser",
                    Description = "There is already a captain for this user"
                });
            }

            return errors;
        }
    }
}