using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripCalculatorService.Entities;

namespace TripCalculatorService.Interfaces
{
    public interface IFriendRepository
    {
        Task<IEnumerable<Friend>> GetAll();
        Task<Friend> Get(string id);
        Task<string> AddFriend(Friend friend);
        Task<string> RemoveFriend(string id);
        Task<string> AddPurchasedItem(string friendId, PurchasedItem purchasedItem);
        Task<string> RemovePurchasedItem(string friendId, Guid purchasedItemId);
        Task<string> UpdatePurchasedItem(string friendId, Guid purchasedItemId, PurchasedItem purchasedItem);
    }
}
