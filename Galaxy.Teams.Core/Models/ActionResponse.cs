using System.Collections.Generic;

namespace Galaxy.Teams.Core.Models
{
    public class ActionResponse
    {
        public bool Success { get; set; } = true;
        public List<ActionError> Errors { get; set; }
    }
}