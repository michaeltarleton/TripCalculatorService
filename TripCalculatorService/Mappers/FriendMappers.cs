using System.Collections.Generic;
using System.Linq;

namespace TripCalculatorService.Mappers
{
    public static class FriendMappers
    {
        public static IEnumerable<Models.Friend> ToModel(this IEnumerable<Entities.Friend> entities)
        {
            return entities.Select(e => e.ToModel());
        }

        public static Models.Friend ToModel(this Entities.Friend entity)
        {
            return entity == null ? null : new Models.Friend {
                       Id             = entity.Id,
                       Name           = entity.Name,
                       PurchasedItems = entity.PurchasedItems.ToModel().ToList()
            };
        }

        public static Entities.Friend ToEntity(this Models.Friend model)
        {
            return model == null ? null : new Entities.Friend {
                       Id             = model.Id,
                       Name           = model.Name,
                       PurchasedItems = model.PurchasedItems.ToEntity().ToList()
            };
        }

        public static IEnumerable<Entities.Friend> ToEntity(this IEnumerable<Models.Friend> models)
        {
            return models == null ? null : models.Select(m => m.ToEntity());
        }
    }

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
