using System.Threading.Tasks;

namespace Galaxy.Teams.Core.Intefaces
{
    public interface IUserGrpcService
    {
        Task<(int, string)> GetUserAsync(string username);
    }
}