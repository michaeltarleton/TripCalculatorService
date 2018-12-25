using entities = TripCalculatorService.Entities;
using models   = TripCalculatorService.Models;
using System.Collections.Generic;
using System.Linq;

namespace TripCalculatorService.Mappers
{
    public static class FriendMappers
    {
        public static IEnumerable <models.Friend> ToModel(this IEnumerable <entities.Friend> entities) { return(entities.ToModel()); }

        public static models.Friend ToModel(this entities.Friend entity)
        {
            return(new models.Friend {
                Name = entity.Name,
                PurchasedItems = entity.PurchasedItems.ToModel().ToList()
            });
        }

        public static models.PurchaseItem ToModel(this entities.PurchasedItem purchasedItem)
        {
            return(new models.PurchaseItem {
                Name = purchasedItem.Name,
                Price = purchasedItem.Price
            });
        }

        public static IEnumerable <models.PurchaseItem> ToModel(this IEnumerable <entities.PurchasedItem> purchaseItem) { return(purchaseItem.Select(i => i.ToModel())); }

        public static IEnumerable <entities.Friend> ToModel(this IEnumerable <models.Friend> models) { return(models.ToModel()); }

        public static entities.Friend ToEntity(this models.Friend model)
        {
            return(new entities.Friend {
                Name = model.Name,
                PurchasedItems = model.PurchasedItems.ToEntity().ToList()
            });
        }

        public static entities.PurchasedItem ToEntity(this models.PurchaseItem purchasedItem)
        {
            return(new entities.PurchasedItem {
                Name = purchasedItem.Name,
                Price = purchasedItem.Price
            });
        }

        public static IEnumerable <entities.PurchasedItem> ToEntity(this IEnumerable <models.PurchaseItem> purchasedItems) { return(purchasedItems.Select(i => i.ToEntity())); }
    }
}
