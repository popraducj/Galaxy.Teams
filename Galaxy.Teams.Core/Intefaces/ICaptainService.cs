using System.Threading.Tasks;
using Galaxy.Teams.Core.Models;

namespace Galaxy.Teams.Core.Intefaces
{
    public interface ICaptainService
    {
        Task<ActionResponse> AddAsync(Captain captain);
    }
}