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
}
