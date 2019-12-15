using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Galaxy.Teams.Core.Models
{
    [Table("Teams")]
    public class Team
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CaptainId { get; set; }
        public Guid ShuttleId { get; set; }
        public List<Guid> Robots { get; set; }
        [Obsolete("Used only for fk")]
        public Captain Captain { get; set; }
        [Obsolete("Used only for fk")]
        public Shuttle Shuttle { get; set; }
    }
}