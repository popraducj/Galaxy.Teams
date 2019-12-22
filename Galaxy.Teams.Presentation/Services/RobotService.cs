using System;
using System.Threading.Tasks;
using Galaxy.Robots.Presentation;
using Galaxy.Teams.Core.Intefaces;
using Galaxy.Teams.Presentation.Helpers;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Robot = Galaxy.Robots.Presentation.Robot;

namespace Galaxy.Teams.Presentation.Services
{
    public class RobotService : Robot.RobotBase
    {
        private readonly ILogger<RobotService> _logger;
        private readonly IService<Core.Models.Robot> _robotService;

        public RobotService(ILogger<RobotService> logger, IService<Core.Models.Robot> robotService)
        {
            _logger = logger;
            _robotService = robotService;
        }

        public override async Task<ActionReplay> Add(RobotModel robotModel, ServerCallContext context)
        {
            var result = await _robotService.AddAsync(robotModel.ToRobot());

            return  result.ToActionReplay();
        }

        public override async Task<ActionReplay> Update(RobotModel robotModel, ServerCallContext context)
        {
            var result = await _robotService.UpdateAsync(robotModel.ToRobot());
            return result.ToActionReplay();
        }

        public override async Task GetAll(Empty request, IServerStreamWriter<RobotModel> responseStream, ServerCallContext context)
        {
            var results = await _robotService.GetAllAsync();
             results.ForEach(async robot => { await responseStream.WriteAsync(robot.ToRobotModel()); });
        }

        public override Task<RobotModel> GetById(IdRequest request, ServerCallContext context)
        {
            var robot = _robotService.GetById(Guid.Parse(request.Id));
            return Task.FromResult(robot.ToRobotModel());
        }
    }
}