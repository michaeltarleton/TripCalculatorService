using System.Collections.Generic;
using System.Threading.Tasks;
using TripCalculatorService.DataAccess;
using TripCalculatorService.Interfaces;
using TripCalculatorService.Mappers;
using TripCalculatorService.Models;

namespace TripCalculatorService.Services
{
    public class FriendService : IFriendService
    {
        private readonly IFriendRepository _repo;

        public FriendService(IFriendRepository repo)
        {
            _repo = repo;
        }

        public async Task<DataAccessResponse<IEnumerable<Friend>>> GetAll()
        {
            return (await _repo.GetAll()).ToModel();
        }

        public async Task<DataAccessResponse<Friend>> Get(string id)
        {
            return (await _repo.Get(id)).ToModel();
        }

        public async Task<DataAccessResponse<string>> Add(Friend friend)
        {
            return (await _repo.Add(friend.ToEntity())).ToModel();
        }

        public async Task<DataAccessResponse<string>> Update(Friend friend)
        {
            return (await _repo.Add(friend.ToEntity())).ToModel();
        }

        public async Task<DataAccessResponse<string>> Remove(string id)
        {
            return (await _repo.Remove(id)).ToModel();
        }
    }
}
