using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Galaxy.Teams.Core.Intefaces;
using Galaxy.Teams.Core.Models;

namespace Galaxy.Teams.Core.Services
{
    public class CaptainService : ICaptainService
    {
        private readonly IRepository<Captain> _repository;
        private readonly IUserGrpcService _userGrpcService;

        public CaptainService(IRepository<Captain> repository, IUserGrpcService userGrpcService)
        {
            _repository = repository;
            _userGrpcService = userGrpcService;
        }

        public async Task<ActionResponse> AddAsync(Captain captain)
        {
            var errors = await ValidateCaptain(captain);
            if (errors.Any())
            {
                return new ActionResponse
                {
                    Errors = errors,
                    Success = false
                };
            }

            captain.UpdatedAt = captain.CreatedAt = DateTime.UtcNow;
            await _repository.AddAsync(captain);
            return new ActionResponse();
        }

        public async Task<ActionResponse> UpdateAsync(Captain captain)
        {
            var dbCaptain = _repository.GetById(captain.Id);
            if (dbCaptain == null)
            {
                return  new ActionResponse
                {
                    Success = false,
                    Errors = new List<ActionError>
                    {
                        new ActionError
                        {
                            Code = "NotFound",
                            Description = "Captain was not found"
                        }
                    }
                };
            }

            dbCaptain.UpdatedAt = DateTime.UtcNow;
            dbCaptain.Status = captain.Status;
            await _repository.UpdateAsync(dbCaptain);
            return new ActionResponse{Success = true};
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

            return errors;
        }
    }
}