using System.Collections.Generic;
using System.Threading.Tasks;
using TripCalculatorService.Models;

namespace TripCalculatorService.Interfaces
{
    public interface IFriendService
    {
        Task<IEnumerable<Friend>> GetAll();
        Task<Friend> Get(string id);
        Task<string> Add(Friend friend);
        Task<string> Remove(string id);
    }
}
