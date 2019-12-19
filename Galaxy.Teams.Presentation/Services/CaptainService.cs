using System.Threading.Tasks;
using Galaxy.Teams.Core.Intefaces;
using Galaxy.Teams.Core.Models;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Galaxy.Teams.Presentation.Services
{
    public class CaptainService: Captain.CaptainBase
    {
        private readonly ILogger<CaptainService> _logger;
        private readonly ICaptainService _captainService;

        public CaptainService(ILogger<CaptainService> logger, ICaptainService captainService)
        {
            _logger = logger;
            _captainService = captainService;
        }

        public override async Task<CaptainActionReplay> AddCaptain(AddCaptainRequest request, ServerCallContext context)
        {
            var result = await _captainService.AddAsync(new Core.Models.Captain
            {
                Age = request.Age,
                Username = request.Username
            });

            return ToCaptainActionReplay(result);
        }


        private static CaptainActionReplay ToCaptainActionReplay(ActionResponse actionResponse)
        {
            var response = new CaptainActionReplay
            {
                Success = actionResponse.Success,
            };

            if (response.Success) return response;
            
            actionResponse.Errors.ForEach(err =>
            {
                response.Errors.Add(new ActionError
                {
                    Code = err.Code,
                    Description = err.Description
                });
            });
            return response;
        }
        
    }
}