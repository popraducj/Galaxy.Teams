using System;
using System.Threading.Tasks;
using Galaxy.Auth.Grpc;
using Galaxy.Teams.Core.Intefaces;
using Grpc.Net.Client;

namespace Galaxy.Teams.Presentation.Services.Users
{
    public class UserService : IUserGrpcService
    {
        private readonly User.UserClient _client;
        public UserService()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            _client = new User.UserClient(channel);
        }
        
        public async Task<(int, string)> GetUserAsync(string username)
        {
            var replay = await _client.GetUserAsync(new UserRequest
            {
                Username = username
            });
            return (replay.Id, replay.Name);
        }
    }
}