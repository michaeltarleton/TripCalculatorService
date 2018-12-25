using System.Collections.Generic;
using System.Net;
using TripCalculatorService.Entities;

namespace TripCalculatorService.DataAccess
{
    public class DataAccessResponse<T>
    {
        public T Payload { get; set; }
        public HttpStatusCode Status { get; set; }

        public DataAccessResponse(T payload, HttpStatusCode status)
        {
        }

        public DataAccessResponse(HttpStatusCode status)
        {
        }
    }

    public static class DataAccessResponse
    {
        public static DataAccessResponse<T> OK<T>(T payload)
        {
            return new DataAccessResponse<T>(payload, HttpStatusCode.OK);
        }

        public static DataAccessResponse<T> NotFound<T>()
        {
            return new DataAccessResponse<T>(HttpStatusCode.NotFound);
        }

        public static DataAccessResponse<T> Created<T>(T payload)
        {
            return new DataAccessResponse<T>(payload, HttpStatusCode.Created);
        }

        public static DataAccessResponse<T> NoContent<T>(T payload)
        {
            return new DataAccessResponse<T>(payload, HttpStatusCode.NoContent);
        }

        public static DataAccessResponse<T> InternalServerError<T>()
        {
            return new DataAccessResponse<T>(HttpStatusCode.InternalServerError);
        }

        public static DataAccessResponse<T> BadRequest<T>()
        {
            return new DataAccessResponse<T>(HttpStatusCode.BadRequest);
        }
    }
}
