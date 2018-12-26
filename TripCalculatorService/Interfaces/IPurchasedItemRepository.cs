using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripCalculatorService.DataAccess;
using TripCalculatorService.Entities;

namespace TripCalculatorService.Interfaces
{
    public interface IPurchasedItemRepository
    {
        Task<DataAccessResponse<PurchasedItem>> GetPurchasedItem(string friendId, Guid purchasedItemId);
        Task<DataAccessResponse<IEnumerable<PurchasedItem>>> GetAllPurchasedItems(string friendId);
        Task<DataAccessResponse<string>> AddPurchasedItem(string friendId, PurchasedItem purchasedItem);
        Task<DataAccessResponse<string>> RemovePurchasedItem(string friendId, Guid purchasedItemId);
        Task<DataAccessResponse<string>> UpdatePurchasedItem(string friendId, Guid purchasedItemId, PurchasedItem purchasedItem);
    }
}
