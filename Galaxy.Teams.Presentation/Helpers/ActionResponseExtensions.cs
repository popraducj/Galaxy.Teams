using Galaxy.Teams.Core.Models;

namespace Galaxy.Teams.Presentation.Helpers
{
    public static class ActionResponseExtensions
    {
        public static ActionReplay ToActionReplay(this ActionResponse actionResponse)
        {
            var response = new ActionReplay
            {
                Success = actionResponse.Success,
            };

            if (response.Success) return response;
            
            actionResponse.Errors.ForEach(err =>
            {
                response.Errors.Add(new ActionError
                {
                    Code = err.Code,
                    Description = err.Description
                });
            });
            return response;
        }
    }
}