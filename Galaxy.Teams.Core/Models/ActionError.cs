namespace Galaxy.Teams.Core.Models
{
    public class ActionError
    {
        public string Code { get; set; }
        public string Description { get; set; }

        public static ActionError NotFound(string name)
        {
            return new ActionError
            {
                Code = "NotFound",
                Description = $"{name} was not found"
            };
        }
        public static ActionError NotAvailableForTeam(string name)
        {
            return new ActionError
            {
                Code = "NotAvailable",
                Description = $"{name} is not available to be assigned to a team"
            };
        }
    }
}