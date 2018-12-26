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

        public async Task<DataAccessResponse<IEnumerable<PurchasedItem>>> GetAll(string friendId)
        {
            return (await _repo.GetAllPurchasedItems(friendId)).ToModel();
        }

        public async Task<DataAccessResponse<PurchasedItem>> Get(string friendId, Guid purchasedItemId)
        {
            return (await _repo.GetPurchasedItem(friendId, purchasedItemId)).ToModel();
        }

        public async Task<DataAccessResponse<string>> Add(string friendId, PurchasedItem purchasedItem)
        {
            return (await _repo.AddPurchasedItem(friendId, purchasedItem.ToEntity())).ToModel();
        }

        public async Task<DataAccessResponse<string>> Remove(string friendId, Guid purchasedItemId)
        {
            return (await _repo.RemovePurchasedItem(friendId, purchasedItemId)).ToModel();
        }

        public async Task<DataAccessResponse<string>> Update(string friendId, Guid purchasedItemId, PurchasedItem purchasedItem)
        {
            return (await _repo.UpdatePurchasedItem(friendId, purchasedItemId, purchasedItem.ToEntity())).ToModel();
        }
    }
}
