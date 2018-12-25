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
    [Route("api/friend/{friendId}/purchased")]
    [ApiController]
    public class PurchasedItemController : ControllerBase
    {
        private readonly IPurchasedItemService _service;

        public PurchasedItemController(IPurchasedItemService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchasedItem>>> Get(string friendId)
        {
            IEnumerable<PurchasedItem> response = await _service.GetAll(friendId);

            if (response == null) return BadRequest();

            return Ok(response);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PurchasedItem>> Get(string friendId, [FromBody] PurchasedItem purchasedItem)
        {
            PurchasedItem response = await _service.Get(friendId, purchasedItem.Name, purchasedItem.Price);

            if (response == null) return BadRequest();

            return Ok(response);
        }

        [HttpPut("")]
        public async Task<ActionResult<string>> Post(string friendId, [FromBody] PurchasedItem purchasedItem)
        {
            string response = await _service.Add(friendId, purchasedItem);

            if (response == null) return BadRequest();

            return Ok(response);
        }

        [HttpDelete("")]
        public async Task<ActionResult<string>> Delete(string friendId, [FromBody] PurchasedItem purchasedItem)
        {
            string response = await _service.Remove(friendId, purchasedItem);

            if (response == null) return BadRequest();

            return Ok(response);
        }
    }
}
