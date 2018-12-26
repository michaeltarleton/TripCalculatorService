using System.Collections.Generic;
using System.Threading.Tasks;
using TripCalculatorService.DataAccess;
using TripCalculatorService.Models;

namespace TripCalculatorService.Interfaces
{
    public interface IFriendService
    {
        Task<DataAccessResponse<IEnumerable<Friend>>> GetAll();
        Task<DataAccessResponse<Friend>> Get(string id);
        Task<DataAccessResponse<string>> Add(Friend friend);
        Task<DataAccessResponse<string>> Remove(string id);
    }
}
