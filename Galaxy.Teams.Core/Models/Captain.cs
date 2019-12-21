using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Galaxy.Teams.Core.Models
{
    [Table("Captains")]
    public class Captain : Entity
    {
        
        public int Age { get; set; }
        public int UserId { get; set; }
        public int Expeditions { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public string Username { get; set; }
        public CaptainStatus Status { get; set; }
    }
}