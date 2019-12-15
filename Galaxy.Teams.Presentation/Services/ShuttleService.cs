using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Galaxy.Teams.Presentation.Services
{
    public class ShuttleService : Shuttle.ShuttleBase
    {
        private readonly ILogger<ShuttleService> _logger;

        public ShuttleService(ILogger<ShuttleService> logger)
        {
            _logger = logger;
        }

        public override Task<ShuttleReply> UpdateShuttle(ShuttleRequest request, ServerCallContext context)
        {
            return base.UpdateShuttle(request, context);
        }
    }
}