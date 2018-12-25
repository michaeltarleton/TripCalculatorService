using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripCalculatorService.Models;

namespace TripCalculatorService.Interfaces
{
    public interface IPurchasedItemService
    {
        Task<IEnumerable<PurchasedItem>> GetAll(string friendId);
        Task<PurchasedItem> Get(string friendId, Guid purchasedItemId);
        Task<string> Add(string friendId, PurchasedItem purchasedItem);
        Task<string> Remove(string friendId, Guid purchasedItemId);
        Task<string> Update(string friendId, Guid purchasedItemId, PurchasedItem purchasedItem);
    }
}
