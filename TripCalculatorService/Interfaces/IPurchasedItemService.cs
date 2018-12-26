using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripCalculatorService.DataAccess;
using TripCalculatorService.Models;

namespace TripCalculatorService.Interfaces
{
    public interface IPurchasedItemService
    {
        Task<DataAccessResponse<IEnumerable<PurchasedItem>>> GetAll(string friendId);
        Task<DataAccessResponse<PurchasedItem>> Get(string friendId, Guid purchasedItemId);
        Task<DataAccessResponse<string>> Add(string friendId, PurchasedItem purchasedItem);
        Task<DataAccessResponse<string>> Remove(string friendId, Guid purchasedItemId);
        Task<DataAccessResponse<string>> Update(string friendId, Guid purchasedItemId, PurchasedItem purchasedItem);
    }
}
