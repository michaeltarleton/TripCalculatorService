using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripCalculatorService.DataAccess;
using TripCalculatorService.Entities;

namespace TripCalculatorService.Interfaces
{
    public interface IFriendRepository
    {
        Task<DataAccessResponse<Friend>> Get(string id);
        Task<DataAccessResponse<IEnumerable<Friend>>> GetAll();
        Task<DataAccessResponse<string>> Add(Friend friend);
        Task<DataAccessResponse<string>> Update(string id, Friend friend);
        Task<DataAccessResponse<string>> Remove(string id);
    }
}
