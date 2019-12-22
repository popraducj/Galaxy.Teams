using System;
using System.Linq;
using System.Threading.Tasks;
using Galaxy.Teams.Core.Intefaces;
using Galaxy.Teams.Core.Models;
using Galaxy.Teams.Presentation.Helpers;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Galaxy.Teams.Presentation.Services
{
    public class CaptainService: Captain.CaptainBase
    {
        private readonly ILogger<CaptainService> _logger;
        private readonly IService<Core.Models.Captain> _captainService;

        public CaptainService(ILogger<CaptainService> logger, IService<Core.Models.Captain> captainService)
        {
            _logger = logger;
            _captainService = captainService;
        }

        public override async Task<ActionReplay> Add(CaptainModel request, ServerCallContext context)
        {
            var result = await _captainService.AddAsync(new Core.Models.Captain
            {
                Age = request.Age,
                Username = request.Username
            });

            return  result.ToActionReplay();
        }

        public override async Task<ActionReplay> Update(CaptainModel request, ServerCallContext context)
        {
            var result = await _captainService.UpdateAsync(new Core.Models.Captain
            {
                Id = Guid.Parse(request.Id),
                Status = (CaptainStatus) request.Status
            });
            return result.ToActionReplay();
        }

        public override async Task GetAll(Empty request, IServerStreamWriter<CaptainModel> responseStream, ServerCallContext context)
        {
            var results = await _captainService.GetAllAsync();
             results.ForEach(async captain => { await responseStream.WriteAsync(ToCaptainModel(captain)); });
        }

        public override Task<CaptainModel> GetById(IdRequest request, ServerCallContext context)
        {
            var captain = _captainService.GetById(Guid.Parse(request.Id));
            return Task.FromResult(ToCaptainModel(captain));
        }

        private static CaptainModel ToCaptainModel(Core.Models.Captain captain)
        {
            return captain == null ? new CaptainModel(): new CaptainModel
            {
                Id = captain.Id.ToString(),
                Expeditions = captain.Expeditions,
                Name = captain.Name,
                Status = (int) captain.Status,    
                Age = captain.Age
            };
        }


        
        
    }
}