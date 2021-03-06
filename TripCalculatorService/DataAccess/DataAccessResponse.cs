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
            Payload = payload;
            Status  = status;
        }

        public DataAccessResponse(HttpStatusCode status)
        {
            Status = status;
        }

        public DataAccessResponse()
        {
        }

        public DataAccessResponse<T> Ok(T payload)
        {
            this.Payload = payload;
            this.Status  = HttpStatusCode.OK;
            return this;
        }

        public DataAccessResponse<T> NotFound()
        {
            this.Status = HttpStatusCode.NotFound;
            return this;
        }

        public DataAccessResponse<T> Created(T payload)
        {
            this.Payload = payload;
            this.Status  = HttpStatusCode.Created;
            return this;
        }

        public DataAccessResponse<T> NoContent(T payload)
        {
            this.Payload = payload;
            this.Status  = HttpStatusCode.NoContent;
            return this;
        }

        public DataAccessResponse<T> InternalServerError()
        {
            this.Status = HttpStatusCode.InternalServerError;
            return this;
        }

        public DataAccessResponse<T> BadRequest()
        {
            this.Status = HttpStatusCode.BadRequest;
            return this;
        }
    }
}
