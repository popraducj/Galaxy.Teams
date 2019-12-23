using System;
using System.Threading.Tasks;
using Galaxy.Shuttles;
using Galaxy.Teams.Core.Intefaces;
using Galaxy.Teams.Presentation.Helpers;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Galaxy.Teams.Presentation.Services
{
    public class ShuttleService : Shuttle.ShuttleBase
    {
        private readonly ILogger<ShuttleService> _logger;
        private readonly IService<Core.Models.Shuttle> _shuttleService;

        public ShuttleService(ILogger<ShuttleService> logger, IService<Core.Models.Shuttle> shuttleService)
        {
            _logger = logger;
            _shuttleService = shuttleService;
        }

        public override async Task<ActionReplay> Add(ShuttleModel shuttleModel, ServerCallContext context)
        {
            var result = await _shuttleService.AddAsync(shuttleModel.ToShuttle());

            return  result.ToActionReplay();
        }

        public override async Task<ActionReplay> Update(ShuttleModel shuttleModel, ServerCallContext context)
        {
            var result = await _shuttleService.UpdateAsync(shuttleModel.ToShuttle());
            return result.ToActionReplay();
        }

        public override async Task GetAll(Empty request, IServerStreamWriter<ShuttleModel> responseStream, ServerCallContext context)
        {
            var results = await _shuttleService.GetAllAsync();
            results.ForEach(async shuttle => { await responseStream.WriteAsync(shuttle.ToShuttleModel()); });
        }

        public override Task<ShuttleModel> GetById(IdRequest request, ServerCallContext context)
        {
            var shuttle = _shuttleService.GetById(Guid.Parse(request.Id));
            return Task.FromResult(shuttle.ToShuttleModel());
        }
    }
}