using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripCalculatorService.DataAccess;
using TripCalculatorService.Interfaces;
using TripCalculatorService.Mappers;
using TripCalculatorService.Models;

namespace TripCalculatorService.Services
{
    public class PurchasedItemService : IPurchasedItemService
    {
        private readonly IFriendRepository _repo;

        public PurchasedItemService(IFriendRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<PurchasedItem>> GetAll(string friendId)
        {
            Friend friend = (await _repo.Get(friendId)).ToModel();

            return friend == null ? null : friend.PurchasedItems;
        }

        public async Task<PurchasedItem> Get(string friendId, string itemName, decimal itemPrice)
        {
            Friend friend = (await _repo.Get(friendId)).ToModel();

            return friend == null ? null : friend.PurchasedItems.FirstOrDefault(p => p.Name == itemName && p.Price == itemPrice);
        }

        public async Task<string> Add(string friendId, PurchasedItem purchasedItem)
        {
            return await _repo.AddPurchasedItem(friendId, purchasedItem.ToEntity());
        }

        public async Task<string> Remove(string friendId, PurchasedItem purchasedItem)
        {
            return await _repo.RemovePurchasedItem(friendId, purchasedItem.ToEntity());
        }
    }
}
