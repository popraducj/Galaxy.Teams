using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Galaxy.Teams.Core.Models
{
    [Table("Robots")]
    public class Robot
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}