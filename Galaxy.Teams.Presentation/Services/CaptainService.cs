using System;
using System.Threading.Tasks;
using Galaxy.Teams.Core.Intefaces;
using Galaxy.Teams.Core.Models;
using Google.Protobuf.WellKnownTypes;
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

        public override async Task<CaptainActionReplay> UpdateCaptain(UpdateCaptainRequest request, ServerCallContext context)
        {
            var result = await _captainService.UpdateAsync(new Core.Models.Captain
            {
                Id = Guid.Parse(request.Id),
                Status = (CaptainStatus) request.Status
            });
            return ToCaptainActionReplay(result);
        }

        public override async Task GetAll(Empty request, IServerStreamWriter<CaptainReplay> responseStream, ServerCallContext context)
        {
            var results = await _captainService.GetAllAsync();
             results.ForEach(async captain => { await responseStream.WriteAsync(ToCaptainReplay(captain)); });
        }

        public override Task<CaptainReplay> GetById(CaptainIdRequest request, ServerCallContext context)
        {
            var captain =  _captainService.GetById(Guid.Parse(request.Id));
            return Task.FromResult(ToCaptainReplay(captain));
        }

        private static CaptainReplay ToCaptainReplay(Core.Models.Captain captain)
        {
            return  new CaptainReplay
            {
                Id = captain.Id.ToString(),
                Expeditions = captain.Expeditions,
                Name = captain.Name,
                Status = (int) captain.Status,    
                Age = captain.Age
            };
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