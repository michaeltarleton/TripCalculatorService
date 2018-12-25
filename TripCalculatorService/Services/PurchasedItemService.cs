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
            var response = await _repo.Get(friendId);

            return response.Payload == null ? null : response.Payload.PurchasedItems.ToModel();
        }

        public async Task<PurchasedItem> Get(string friendId, Guid purchasedItemId)
        {
            var response = await _repo.Get(friendId);

            return response.Payload == null ? null : response.Payload.PurchasedItems.ToModel().FirstOrDefault(p => p.Id == purchasedItemId);
        }

        public async Task<string> Add(string friendId, PurchasedItem purchasedItem)
        {
            var response = await _repo.AddPurchasedItem(friendId, purchasedItem.ToEntity());

            return response.Payload == null ? null : response.Payload;
        }

        public async Task<string> Remove(string friendId, Guid purchasedItemId)
        {
            var response = await _repo.RemovePurchasedItem(friendId, purchasedItemId);

            return response.Payload;
        }

        public async Task<string> Update(string friendId, Guid purchasedItemId, PurchasedItem purchasedItem)
        {
            var response = await _repo.UpdatePurchasedItem(friendId, purchasedItemId, purchasedItem.ToEntity());

            return response.Payload;
        }
    }
}
