using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Galaxy.Teams.Core.Models
{
    [Table("Captains")]
    public class Captain
    {
        [Key]
        public Guid Id { get; set; }
        public int Age { get; set; }
        public int UserId { get; set; }
        public int Expeditions { get; set; }
        public CaptainStatus Status { get; set; }
    }
}