using Microsoft.AspNetCore.Mvc;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TripCalculatorService.DataAccess;
using TripCalculatorService.Interfaces;
using TripCalculatorService.Mappers;
using TripCalculatorService.Models;

namespace TripCalculatorService.Controllers
{
    [Route("api/friend/{friendId}/purchased")]
    [ApiController]
    public class PurchasedItemController : BaseApiController
    {
        private readonly IPurchasedItemService _service;

        public PurchasedItemController(IPurchasedItemService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchasedItem>>> Get(string friendId)
        {
            DataAccessResponse<IEnumerable<PurchasedItem>> response = await _service.GetAll(friendId);

            return HandleResponse(response);
        }

        // GET api/values/5
        [HttpGet("{purchasedItemId}")]
        public async Task<ActionResult<PurchasedItem>> Get(string friendId, Guid purchasedItemId)
        {
            DataAccessResponse<PurchasedItem> response = await _service.Get(friendId, purchasedItemId);

            return HandleResponse(response);
        }

        [HttpPost("")]
        public async Task<ActionResult<string>> Post(string friendId, [FromBody] PurchasedItem purchasedItem)
        {
            DataAccessResponse<string> response = await _service.Add(friendId, purchasedItem);

            return HandleResponse(response);
        }

        [HttpPut("{purchasedItemId}")]
        public async Task<ActionResult<string>> Put(string friendId, Guid purchasedItemId, [FromBody] PurchasedItem purchasedItem)
        {
            DataAccessResponse<string> response = await _service.Update(friendId, purchasedItemId, purchasedItem);

            return HandleResponse(response);
        }

        [HttpDelete("{purchasedItemId}")]
        public async Task<ActionResult<string>> Delete(string friendId, Guid purchasedItemId)
        {
            DataAccessResponse<string> response = await _service.Remove(friendId, purchasedItemId);

            return HandleResponse(response);
        }
    }
}
