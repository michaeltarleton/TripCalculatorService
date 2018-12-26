using System.Collections.Generic;
using TripCalculatorService.DataAccess;

namespace TripCalculatorService.Mappers
{
    public static class DataAccessResponseMappers
    {
        public static DataAccessResponse<Entities.Friend> ToEntity(this DataAccessResponse<Models.Friend> response)
        {
            return new DataAccessResponse<Entities.Friend>(response.Payload.ToEntity(), response.Status);
        }

        public static DataAccessResponse<IEnumerable<Entities.Friend>> ToEntity(this DataAccessResponse<IEnumerable<Models.Friend>> response)
        {
            return new DataAccessResponse<IEnumerable<Entities.Friend>>(response.Payload.ToEntity(), response.Status);
        }

        public static DataAccessResponse<Models.Friend> ToModel(this DataAccessResponse<Entities.Friend> response)
        {
            return new DataAccessResponse<Models.Friend>(response.Payload.ToModel(), response.Status);
        }

        public static DataAccessResponse<IEnumerable<Models.Friend>> ToModel(this DataAccessResponse<IEnumerable<Entities.Friend>> response)
        {
            return new DataAccessResponse<IEnumerable<Models.Friend>>(response.Payload.ToModel(), response.Status);
        }
    }
}
