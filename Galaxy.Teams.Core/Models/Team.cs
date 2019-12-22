using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Galaxy.Teams.Core.Enums;
using Galaxy.Teams.Core.Intefaces;

namespace Galaxy.Teams.Core.Models
{
    [Table("Teams")]
    public class Team: Entity
    {
        private readonly IService<Captain> _captainService;
        private readonly IService<Robot> _robotService;
        private readonly IService<Shuttle> _shuttleService;

        public Team(){}
        public Team(IService<Captain> captainService, IService<Robot> robotService, IService<Shuttle> shuttleService)
        {
            _captainService = captainService;
            _robotService = robotService;
            _shuttleService = shuttleService;
        }
        public string Name { get; set; }
        public TeamStatus Status { get; set; }
        public Guid CaptainId { get; set; }
        public Guid ShuttleId { get; set; }
        public List<Guid> Robots { get; set; }
        
        [Obsolete("Used only for fk")]
        public Captain Captain { get; set; }
        [Obsolete("Used only for fk")]
        public Shuttle Shuttle { get; set; }
        
    }
}