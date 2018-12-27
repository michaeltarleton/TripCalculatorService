using Microsoft.AspNetCore.Mvc;
using System.Net;
using TripCalculatorService.DataAccess;

namespace TripCalculatorService.Controllers
{
    public abstract class BaseApiController : ControllerBase
    {
        protected ActionResult<T> HandleResponse<T>(DataAccessResponse<T> response)
        {
            if (response == null) new StatusCodeResult(500);

            switch (response.Status)
            {
                case HttpStatusCode.OK:
                    return Ok(response.Payload);
                case HttpStatusCode.NotFound:
                    return NotFound();
                case HttpStatusCode.Created:
                    return Created(response.Payload as string, response.Payload);
                case HttpStatusCode.NoContent:
                    return NoContent();
                case HttpStatusCode.InternalServerError:
                    return new StatusCodeResult(500);
                default:
                    return BadRequest();
            }
        }
    }
}
