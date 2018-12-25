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
    [Route("api/friend")]
    [ApiController]
    public class FriendController : ControllerBase
    {
        private readonly IFriendRepository _repo;
        public FriendController(IFriendRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task <ActionResult <IEnumerable <Friend> > > Get()
        {
            return Ok((await _repo.GetAll()).ToModel());
        }

        // GET api/friend/5
        [HttpGet("{id}")]
        public async Task <ActionResult <Friend> > Get(string id)
        {
            return Ok((await _repo.Get(id)).ToModel());
        }

        // POST api/friend
        [HttpPost]
        public async Task <ActionResult> Post([FromBody] Friend friend)
        {
            var result = await _repo.AddFriend(friend.ToEntity());

            return Ok();
        }

        // DELETE api/friend/5
        [HttpDelete("{id}")]
        public async Task <ActionResult> Delete(string id)
        {
            var response = await _repo.RemoveFriend(id);

            return Ok();
        }
    }
}
