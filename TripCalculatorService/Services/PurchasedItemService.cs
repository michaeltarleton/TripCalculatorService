using System;
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

        public async Task<PurchasedItem> Get(string friendId, Guid purchasedItemId)
        {
            Friend friend = (await _repo.Get(friendId)).ToModel();

            return friend == null ? null : friend.PurchasedItems.FirstOrDefault(p => p.Id == purchasedItemId);
        }

        public async Task<string> Add(string friendId, PurchasedItem purchasedItem)
        {
            return await _repo.AddPurchasedItem(friendId, purchasedItem.ToEntity());
        }

        public async Task<string> Remove(string friendId, Guid purchasedItemId)
        {
            return await _repo.RemovePurchasedItem(friendId, purchasedItemId);
        }

        public async Task<string> Update(string friendId, Guid purchasedItemId, PurchasedItem purchasedItem)
        {
            return await _repo.UpdatePurchasedItem(friendId, purchasedItemId, purchasedItem.ToEntity());
        }
    }
}
