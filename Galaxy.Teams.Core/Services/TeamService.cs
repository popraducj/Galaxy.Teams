using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Galaxy.Teams.Core.Enums;
using Galaxy.Teams.Core.Intefaces;
using Galaxy.Teams.Core.Models;
using Microsoft.Extensions.Logging;

namespace Galaxy.Teams.Core.Services
{
    public class TeamService : IService<Team>
    {
        private readonly IRepository<Team> _repository;
        private readonly IService<Captain> _captainService;
        private readonly IService<Robot> _robotService;
        private readonly IService<Shuttle> _shuttleService;
        private readonly ILogger<TeamService> _logger;

        public TeamService(IRepository<Team> repository, IService<Captain> captainService, IService<Robot> robotService,
            IService<Shuttle> shuttleService, ILogger<TeamService> logger)
        {
            _repository = repository;
            _captainService = captainService;
            _robotService = robotService;
            _shuttleService = shuttleService;
            _logger = logger;
        }

        public async Task<ActionResponse> AddAsync(Team model)
        {
            var errors = Validate(model, Guid.Empty, Guid.Empty, null);
            if (errors.Any())
            {
                return new ActionResponse {Success = false, Errors = errors};
            }

            await UpdateCaptainRobotsAndShuttle(model);
            
            model.UpdatedAt = model.CreatedAt = DateTime.UtcNow;
            return await _repository.AddAsync(model);
        }

        public async Task<ActionResponse> UpdateAsync(Team model)
        {
            var errors = Validate(model, Guid.Empty, Guid.Empty, null);
            if (errors.Any())
            {
                return new ActionResponse {Success = false, Errors = errors};
            }
            
            await UpdateCaptainRobotsAndShuttle(model);
            
            model.UpdatedAt = DateTime.UtcNow;
            return await _repository.UpdateAsync(model);
        }

        public async Task<List<Team>> GetAllAsync()
        {
            var teams = await _repository.GetAsync(x => x.Status != TeamStatus.Deleted,
                x => x.OrderByDescending(y => y.UpdatedAt));
            return teams.ToList();
        }

        public Team GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        #region Team validation

        private async Task UpdateCaptainRobotsAndShuttle(Team model)
        {
            var captain = _captainService.GetById(model.CaptainId);
            captain.Status = CaptainStatus.HasTeam;
            await _captainService.UpdateAsync(captain);
            
            var shuttle = _shuttleService.GetById(model.ShuttleId);
            shuttle.Status = ShuttleStatus.Assigned;
            await _shuttleService.UpdateAsync(shuttle);

            foreach (var dbRobot in model.Robots.Select(robot => _robotService.GetById(robot)))
            {
                dbRobot.Status = RobotStatus.Exploring;
                await _robotService.UpdateAsync(dbRobot);
            }
        }
        private List<ActionError> Validate(Team team, Guid oldCaptainId, Guid oldShuttleId, List<Guid> oldRobotsIds)
        {
            var response = new List<ActionError>();
            
            response.AddRange(ValidateCaptain(team.CaptainId, oldCaptainId));
            response.AddRange(ValidateShuttle(team.ShuttleId, oldShuttleId));
            response.AddRange(ValidateRobots(team.Robots, oldRobotsIds));

            return response;
        }

        private IEnumerable<ActionError> ValidateCaptain(Guid captainId, Guid oldCaptainId)
        {
            var response = new List<ActionError>();
            if (oldCaptainId == captainId) return response;

            var captain = _captainService.GetById(captainId);
            if (captain == null)
                response.Add(ActionError.NotFound("Captain"));
            
            if (captain != null && captain.Status != CaptainStatus.Unassigned)
                response.Add(ActionError.NotAvailableForTeam("Captain"));

            return response;
        }

        private IEnumerable<ActionError> ValidateShuttle(Guid shuttleId, Guid oldShuttleId)
        {
            var response = new List<ActionError>();
            if (oldShuttleId == shuttleId) return response;

            var shuttle = _shuttleService.GetById(shuttleId);
            if (shuttle == null)
                response.Add(ActionError.NotFound("Shuttle"));
            
            if (shuttle != null && shuttle.Status != ShuttleStatus.Unassigned)
                response.Add(ActionError.NotAvailableForTeam("Shuttle"));
            
            return response;
        }


        private IEnumerable<ActionError> ValidateRobots(List<Guid> robots, List<Guid> oldRobotsIds)
        {
            var response = new List<ActionError>();

            var sameRobots = robots.Intersect(oldRobotsIds ?? new List<Guid>()).ToList();
            if (sameRobots.Count == 5) return response;
            
            if(robots.Count != 5)
                response.Add(new ActionError
                {
                    Code = "5Robots",
                    Description = "There should be exactly 5 robots in the team"
                });

            var newRobots = robots.Except(sameRobots);
            foreach (var robot in newRobots)
            {
                var dbRobot = _robotService.GetById(robot);
                var errorMessage = $"Robot with id {robot}";
                if (dbRobot == null)
                {
                    response.Add(ActionError.NotFound(errorMessage));
                    continue;
                }
                
                if (dbRobot.Status != RobotStatus.Unassigned)
                    response.Add(ActionError.NotAvailableForTeam(errorMessage));
            }

            return response;
        }

        #endregion
    }
}