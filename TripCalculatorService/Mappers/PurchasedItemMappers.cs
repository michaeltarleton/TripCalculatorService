using System.Collections.Generic;
using System.Linq;

namespace TripCalculatorService.Mappers
{
    public static class PurchasedItemMappers
    {
        public static Models.PurchasedItem ToModel(this Entities.PurchasedItem purchasedItem)
        {
            return purchasedItem == null ? null : new Models.PurchasedItem {
                       Id    = purchasedItem.Id,
                       Name  = purchasedItem.Name,
                       Price = purchasedItem.Price
            };
        }

        public static IEnumerable<Models.PurchasedItem> ToModel(this IEnumerable<Entities.PurchasedItem> purchaseItem)
        {
            return purchaseItem == null ? null : purchaseItem.Select(i => i.ToModel());
        }

        public static Entities.PurchasedItem ToEntity(this Models.PurchasedItem purchasedItem)
        {
            return purchasedItem == null ? null : new Entities.PurchasedItem {
                       Id    = purchasedItem.Id,
                       Name  = purchasedItem.Name,
                       Price = purchasedItem.Price
            };
        }

        public static IEnumerable<Entities.PurchasedItem> ToEntity(this IEnumerable<Models.PurchasedItem> purchasedItems)
        {
            return purchasedItems == null ? null : purchasedItems.Select(i => i.ToEntity());
        }
    }
}
