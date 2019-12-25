using System;
using Galaxy.Robots;
using Galaxy.Teams.Core.Enums;
using Robot = Galaxy.Teams.Core.Models.Robot;

namespace Galaxy.Teams.Presentation.Helpers
{
    public static class RobotExtensions
    {
        public static RobotModel ToRobotModel(this Robot robot)
        {
            if(robot == null) return new RobotModel();
            return new RobotModel
            {
                Id = robot.Id.ToString(),
                Name = robot.Name ?? string.Empty,
                Status = (int) robot.Status,
                Manufacturer = robot.Manufacturer ?? string.Empty,
                Model = robot.Model ?? string.Empty,
                Year = robot.Year,
                NextRevision = robot.NextRevision.ToString("s"),
                TrustWorthyPercentage = robot.TrustWorthyPercentage,
                FuelConsumptionPerDay = robot.FuelConsumptionPerDay,
                UnitsCoveredInADay = robot.UnitsCoveredInADay
            };
        }
        public static Robot ToRobot(this RobotModel robot)
        {
            var result =  new Robot
            {
                Id = Guid.Parse(robot.Id),
                Name = robot.Name,
                Status = (RobotStatus) robot.Status,
                Manufacturer = robot.Manufacturer,
                Model = robot.Model,
                Year = robot.Year,
                TrustWorthyPercentage = robot.TrustWorthyPercentage,
                FuelConsumptionPerDay = robot.FuelConsumptionPerDay,
                UnitsCoveredInADay = robot.UnitsCoveredInADay
            };
            if (!string.IsNullOrEmpty(robot.NextRevision))
                result.NextRevision = DateTime.Parse(robot.NextRevision);

            return result;
        }
    }
}