﻿using System;
using System.Collections.Generic;
using Galaxy.Teams.Core.Enums;

namespace Galaxy.Teams.Presentation.Helpers
{
    public static class TeamExtensions
    {
        public static TeamModel ToTeamModel(this Core.Models.Team team)
        {
            if(team == null) return new TeamModel();
            var  response = new TeamModel
            {
                Id = team.Id.ToString(),
                Name = team.Name ?? string.Empty,
                Status = (int) team.Status,
                CaptainId = team.CaptainId.ToString(),
                ShuttleId = team.ShuttleId.ToString()
            };
            team.Robots.ForEach(robot => response.RobotsIds.Add(robot.ToString()));
            return response;
        }
        public static Core.Models.Team ToTeam(this TeamModel team)
        {
            //team.CaptainId = string.IsNullOrEmpty(team.CaptainId) ? Guid.Empty.ToString() : team.CaptainId;
            //team.ShuttleId = string.IsNullOrEmpty(team.ShuttleId) ? Guid.Empty.ToString() : team.ShuttleId;
            var response = new Core.Models.Team
            {
                Id = Guid.Parse(team.Id),
                Name = team.Name,
                Status = (TeamStatus) team.Status,
                CaptainId = Guid.Parse(team.CaptainId),
                ShuttleId = Guid.Parse(team.ShuttleId),
                Robots = new List<Guid>()
            };
            foreach (var robotId in team.RobotsIds)
            {
                response.Robots.Add(Guid.Parse(robotId));
            }

            return response;
        }
    }
}