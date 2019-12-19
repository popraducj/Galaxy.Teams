using System;
using System.ComponentModel.DataAnnotations;

namespace Galaxy.Teams.Core.Models
{
    public class Entity
    {
        [Key]
        public Guid Id { get; set; }
    }
}