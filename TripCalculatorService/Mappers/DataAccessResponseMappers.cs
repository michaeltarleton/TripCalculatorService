using System.Collections.Generic;
using TripCalculatorService.DataAccess;

namespace TripCalculatorService.Mappers
{
    public static class DataAccessResponseMappers
    {
        public static DataAccessResponse<Models.Friend> ToModel(this DataAccessResponse<Entities.Friend> response)
        {
            if (response == null || response.Payload == null) return null;
            return new DataAccessResponse<Models.Friend>(response.Payload.ToModel(), response.Status);
        }

        public static DataAccessResponse<IEnumerable<Models.Friend>> ToModel(this DataAccessResponse<IEnumerable<Entities.Friend>> response)
        {
            if (response == null || response.Payload == null) return null;
            return new DataAccessResponse<IEnumerable<Models.Friend>>(response.Payload.ToModel(), response.Status);
        }

        public static DataAccessResponse<Models.PurchasedItem> ToModel(this DataAccessResponse<Entities.PurchasedItem> response)
        {
            if (response == null || response.Payload == null) return null;
            return new DataAccessResponse<Models.PurchasedItem>(response.Payload.ToModel(), response.Status);
        }

        public static DataAccessResponse<IEnumerable<Models.PurchasedItem>> ToModel(this DataAccessResponse<IEnumerable<Entities.PurchasedItem>> response)
        {
            if (response == null || response.Payload == null) return null;
            return new DataAccessResponse<IEnumerable<Models.PurchasedItem>>(response.Payload.ToModel(), response.Status);
        }

        public static DataAccessResponse<string> ToModel(this DataAccessResponse<string> response)
        {
            if (response == null || response.Payload == null) return null;
            return new DataAccessResponse<string>(response.Payload, response.Status);
        }
    }
}
