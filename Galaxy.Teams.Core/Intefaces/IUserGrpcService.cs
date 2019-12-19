using System.Threading.Tasks;

namespace Galaxy.Teams.Core.Intefaces
{
    public interface IUserGrpcService
    {
        Task<int> VerifyIfUserExistsAsync(string username);
    }
}