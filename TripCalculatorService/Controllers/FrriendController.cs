using Microsoft.AspNetCore.Mvc;
using Nest;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripCalculatorService.DataAccess;
using TripCalculatorService.Interfaces;
using TripCalculatorService.Mappers;
using TripCalculatorService.Models;

namespace TripCalculatorService.Controllers
{
    [Route("api/friend")]
    [ApiController]
    public class FriendController : BaseApiController
    {
        private readonly IFriendService _service;

        public FriendController(IFriendService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Friend>>> Get()
        {
            DataAccessResponse<IEnumerable<Friend>> response = await _service.GetAll();

            return HandleResponse(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Friend>> Get(string id)
        {
            DataAccessResponse<Friend> response = await _service.Get(id);

            return HandleResponse(response);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post([FromBody] Friend friend)
        {
            DataAccessResponse<string> response = await _service.Add(friend);

            return HandleResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete(string id)
        {
            DataAccessResponse<string> response = await _service.Remove(id);

            return HandleResponse(response);
        }
    }
}
