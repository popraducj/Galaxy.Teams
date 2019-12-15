﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Galaxy.Teams.Core.Models
{
    [Table("Robots")]
    public class Robot
    {
        [Key]
        public Guid Id { get; set; }
        [Column(TypeName = "varchar(256)")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(256)")]
        public string Manufacturer { get; set; }
        [Column(TypeName = "varchar(256)")]
        public string Model { get; set; }
        public int Year { get; set; }
        public float UnitsCoveredInADay { get; set; }
        public float TrustWorthyPercentage { get; set; }
        public float FuelConsumptionPerDay { get; set; }
        public DateTime NextRevision { get; set; }
    }
}