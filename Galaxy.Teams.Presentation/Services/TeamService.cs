using System;
using System.Threading.Tasks;
using Galaxy.Teams.Core.Intefaces;
using Galaxy.Teams.Presentation.Helpers;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Galaxy.Teams.Presentation.Services
{
    public class TeamService  : Team.TeamBase
    {
        private readonly ILogger<TeamService> _logger;
        private readonly IService<Core.Models.Team> _teamService;

        public TeamService(ILogger<TeamService> logger, IService<Core.Models.Team> teamService)
        {
            _logger = logger;
            _teamService = teamService;
        }

        public override async Task<ActionReplay> Add(TeamModel teamModel, ServerCallContext context)
        {
            var result = await _teamService.AddAsync(teamModel.ToTeam());

            return  result.ToActionReplay();
        }

        public override async Task<ActionReplay> Update(TeamModel teamModel, ServerCallContext context)
        {
            var result = await _teamService.UpdateAsync(teamModel.ToTeam());
            return result.ToActionReplay();
        }
        
        public override async Task<ActionReplay> UpdateStatus(TeamModel teamModel, ServerCallContext context)
        {
            var result = await _teamService.UpdateStatusAsync(teamModel.ToTeam());
            return result.ToActionReplay();
        }

        public override async Task GetAll(Empty request, IServerStreamWriter<TeamModel> responseStream, ServerCallContext context)
        {
            var results = await _teamService.GetAllAsync();
            results.ForEach(async team => { await responseStream.WriteAsync(team.ToTeamModel()); });
        }

        public override Task<TeamModel> GetById(IdRequest request, ServerCallContext context)
        {
            var team = _teamService.GetById(Guid.Parse(request.Id));
            return Task.FromResult(team.ToTeamModel());
        }
    }
}
