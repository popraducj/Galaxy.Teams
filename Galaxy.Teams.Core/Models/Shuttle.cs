﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Galaxy.Teams.Core.Models
{
    [Table("Shuttles")]
    public class Shuttle : Entity
    {
        [Required]
        [Column(TypeName = "varchar(256)")]
        public string Name { get; set; }
        [Required]
        public int MaxSpeed { get; set; }
        [Required]
        [Column(TypeName = "varchar(256)")]
        public string Manufacturer { get; set; }
        [Column(TypeName = "varchar(256)")]
        public string Model { get; set; }
        public int Year { get; set; }
        public int FuelConsumption { get; set; }
        public int FuelTankLimit { get; set; }
        public DateTime NextRevision { get; set; }
        public ShuttleStatus Status { get; set; }
    }
}