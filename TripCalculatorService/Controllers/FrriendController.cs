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
    [Route("api/friends")]
    [ApiController]
    public class FriendController : ControllerBase
    {
        private readonly IFriendService _service;

        public FriendController(IFriendService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Friend>>> Get()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Friend>> Get(string id)
        {
            return Ok(await _service.Get(id));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Friend friend)
        {
            string response = await _service.Add(friend);

            if (response == null) return BadRequest();

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var response = await _service.Remove(id);

            if (response == null) return BadRequest();

            return Ok(response);
        }
    }
}
