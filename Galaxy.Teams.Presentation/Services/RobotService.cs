using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Galaxy.Teams.Presentation.Services
{
    public class RobotService : Robot.RobotBase
    {
        private readonly ILogger<RobotService> _logger;

        public RobotService(ILogger<RobotService> logger)
        {
            _logger = logger;
        }

        public override Task<RobotReply> UpdateRobot(RobotRequest request, ServerCallContext context)
        {
            return base.UpdateRobot(request, context);
        }
    }
}