using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Galaxy.Teams.Presentation.Services
{
    public class TeamService  : Team.TeamBase
    {
        private readonly ILogger<TeamService> _logger;
        public TeamService(ILogger<TeamService> logger)
        {
            _logger = logger;
        }

        public override Task<TeamReply> UpdateTeam(TeamRequest request, ServerCallContext context)
        {
            return base.UpdateTeam(request, context);
        }
    }
}
