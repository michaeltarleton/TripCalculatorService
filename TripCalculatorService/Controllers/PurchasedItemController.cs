using Microsoft.AspNetCore.Mvc;
using Nest;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripCalculatorService.DataAccess;
using TripCalculatorService.Mappers;
using TripCalculatorService.Models;

namespace TripCalculatorService.Controllers
{
    [Route("api/friend/{id}/purchased")]
    [ApiController]
    public class PurchasedItemController
    {
        private readonly IFriendRepository _repo;
        public PurchasedItemController(IFriendRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task <IEnumerable <Friend> > Get() { return((await _repo.GetAll()).ToModel()); }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task <Friend> Get(string id) { return((await _repo.Get(id)).ToModel()); }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value) { }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Friend friend) { }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id) { }
    }
}
