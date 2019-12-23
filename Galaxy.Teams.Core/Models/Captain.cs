using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Galaxy.Teams.Core.Models
{
    [Table("Captains")]
    public class Captain : Entity
    {
        [Required]
        public int Age { get; set; }
        [Required]
        public int UserId { get; set; }
        public int Expeditions { get; set; }
        [Required]
        [Column(TypeName = "varchar(256)")]
        public string Name { get; set; } = string.Empty;
        [NotMapped]
        public string Username { get; set; }
        public CaptainStatus Status { get; set; }
    }
}