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
        Task<string> AddPurchasedItem(string friendId, PurchasedItem item);
        Task<string> RemovePurchasedItem(string friendId, PurchasedItem item);
    }
}
