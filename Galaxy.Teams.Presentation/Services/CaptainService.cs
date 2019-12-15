using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Galaxy.Teams.Presentation.Services
{
    public class CaptainService: Captain.CaptainBase
    {
        private readonly ILogger<CaptainService> _logger;

        public CaptainService(ILogger<CaptainService> logger)
        {
            _logger = logger;
        }

        public override Task<CaptainReply> UpdateCaptain(CaptainRequest request, ServerCallContext context)
        {
            return base.UpdateCaptain(request, context);
        }
    }
}