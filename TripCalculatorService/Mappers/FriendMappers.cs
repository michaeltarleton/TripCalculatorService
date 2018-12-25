using entities = TripCalculatorService.Entities;
using models   = TripCalculatorService.Models;
using System.Collections.Generic;
using System.Linq;

namespace TripCalculatorService.Mappers
{
    public static class FriendMappers
    {
        public static IEnumerable<models.Friend> ToModel(this IEnumerable<entities.Friend> entities)
        {
            return entities.Select(e => e.ToModel());
        }

        public static models.Friend ToModel(this entities.Friend entity)
        {
            return entity == null ? null : new models.Friend {
                       Id             = entity.Id,
                       Name           = entity.Name,
                       PurchasedItems = entity.PurchasedItems.ToModel().ToList()
            };
        }

        public static entities.Friend ToEntity(this models.Friend model)
        {
            return model == null ? null : new entities.Friend {
                       Id             = model.Id,
                       Name           = model.Name,
                       PurchasedItems = model.PurchasedItems.ToEntity().ToList()
            };
        }

        public static IEnumerable<entities.Friend> ToModel(this IEnumerable<models.Friend> models)
        {
            return models == null ? null : models.Select(m => m.ToEntity());
        }
    }

    public static class PurchasedItemMappers
    {
        public static models.PurchasedItem ToModel(this entities.PurchasedItem purchasedItem)
        {
            return purchasedItem == null ? null : new models.PurchasedItem {
                       Id    = purchasedItem.Id,
                       Name  = purchasedItem.Name,
                       Price = purchasedItem.Price
            };
        }

        public static IEnumerable<models.PurchasedItem> ToModel(this IEnumerable<entities.PurchasedItem> purchaseItem)
        {
            return purchaseItem == null ? null : purchaseItem.Select(i => i.ToModel());
        }

        public static entities.PurchasedItem ToEntity(this models.PurchasedItem purchasedItem)
        {
            return purchasedItem == null ? null : new entities.PurchasedItem {
                       Id    = purchasedItem.Id,
                       Name  = purchasedItem.Name,
                       Price = purchasedItem.Price
            };
        }

        public static IEnumerable<entities.PurchasedItem> ToEntity(this IEnumerable<models.PurchasedItem> purchasedItems)
        {
            return purchasedItems == null ? null : purchasedItems.Select(i => i.ToEntity());
        }
    }
}
