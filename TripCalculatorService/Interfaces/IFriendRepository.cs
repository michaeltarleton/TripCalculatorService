using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripCalculatorService.DataAccess;
using TripCalculatorService.Entities;

namespace TripCalculatorService.Interfaces
{
    public interface IFriendRepository
    {
        Task<DataAccessResponse<IEnumerable<Friend>>> GetAll();
        Task<DataAccessResponse<Friend>> Get(string id);
        Task<DataAccessResponse<string>> AddFriend(Friend friend);
        Task<DataAccessResponse<string>> UpdateFriend(string id, Friend friend);
        Task<DataAccessResponse<string>> RemoveFriend(string id);
        Task<DataAccessResponse<string>> AddPurchasedItem(string friendId, PurchasedItem purchasedItem);
        Task<DataAccessResponse<string>> RemovePurchasedItem(string friendId, Guid purchasedItemId);
        Task<DataAccessResponse<string>> UpdatePurchasedItem(string friendId, Guid purchasedItemId, PurchasedItem purchasedItem);
    }
}
