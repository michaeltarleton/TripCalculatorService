using Microsoft.AspNetCore.Mvc;
using System.Net;
using TripCalculatorService.DataAccess;

namespace TripCalculatorService.Controllers
{
    public abstract class BaseApiController : ControllerBase
    {
        protected ActionResult<T> HandleResponse<T>(DataAccessResponse<T> response)
        {
            switch (response.Status)
            {
                case HttpStatusCode.OK:
                    return Ok(response.Payload);
                case HttpStatusCode.NotFound:
                    return NotFound();
                default:
                    return new StatusCodeResult((int)response.Status);
            }
        }
    }
}
