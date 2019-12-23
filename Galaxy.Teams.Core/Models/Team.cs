using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Galaxy.Teams.Core.Enums;

namespace Galaxy.Teams.Core.Models
{
    [Table("Teams")]
    public class Team: Entity
    {
        [Required]
        [Column(TypeName = "varchar(256)")]
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