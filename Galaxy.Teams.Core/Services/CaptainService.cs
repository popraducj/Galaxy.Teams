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

            await _repository.AddAsync(captain);
            return new ActionResponse();
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

            var userId = await _userGrpcService.VerifyIfUserExistsAsync(captain.Username);
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
            }

            return errors;
        }
    }
}