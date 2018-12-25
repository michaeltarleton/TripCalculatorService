using System.Collections.Generic;
using System.Threading.Tasks;
using TripCalculatorService.Models;

namespace TripCalculatorService.Interfaces
{
    public interface IPurchasedItemService
    {
        Task<IEnumerable<PurchasedItem>> GetAll(string friendId);
        Task<PurchasedItem> Get(string friendId, string itemName, decimal itemPrice);
        Task<string> Add(string friendId, PurchasedItem purchasedItem);
        Task<string> Remove(string friendId, PurchasedItem purchasedItem);
    }
}
