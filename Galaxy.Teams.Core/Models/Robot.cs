using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Galaxy.Teams.Core.Enums;

namespace Galaxy.Teams.Core.Models
{
    [Table("Robots")]
    public class Robot: Entity
    {
        [Column(TypeName = "varchar(256)")] public string Name { get; set; }
        public RobotStatus Status { get; set; }
        [Column(TypeName = "varchar(256)")] public string Manufacturer { get; set; }
        [Column(TypeName = "varchar(256)")] public string Model { get; set; }
        public int Year { get; set; }
        public int UnitsCoveredInADay { get; set; }
        public int TrustWorthyPercentage { get; set; }
        public int FuelConsumptionPerDay { get; set; }
        public DateTime NextRevision { get; set; }
    }
}