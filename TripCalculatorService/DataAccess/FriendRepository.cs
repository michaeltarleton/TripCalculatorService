using Elasticsearch.Net;
using Nest;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripCalculatorService.Entities;
using TripCalculatorService.Interfaces;

namespace TripCalculatorService.DataAccess
{
    public class FriendRepository : IFriendRepository
    {
        private IElasticClient _esClient;
        private string _index = "friends";
        private string _type  = "friend";

        public FriendRepository(IElasticClient esClient)
        {
            _esClient = esClient;
        }

        public async Task<IEnumerable<Friend>> GetAll()
        {
            var result = await _esClient.SearchAsync<Friend>(s => s);

            return result.Documents;
        }

        public async Task<Friend> Get(string id)
        {
            IGetResponse<Friend> response = await _esClient.GetAsync<Friend>(id);

            return response.Source;
        }

        public async Task<string> AddPurchasedItem(string friendId, PurchasedItem item)
        {
            Friend friend = await this.Get(friendId);

            friend.PurchasedItems.Add(item);

            IUpdateResponse<Friend> response = await _esClient.UpdateAsync<Friend>(friendId, d => d.Index(_index).Type(_type).Doc(friend));

            return response.IsValid ? response.Id : null;
        }

        public async Task<string> RemovePurchasedItem(string friendId, PurchasedItem item)
        {
            Friend friend = await this.Get(friendId);

            friend.PurchasedItems.Remove(item);

            IUpdateResponse<Friend> response = await _esClient.UpdateAsync<Friend>(friendId, d => d.Index(_index).Type(_type).Doc(friend));

            return response.IsValid ? response.Id : null;
        }

        public async Task<string> AddFriend(Friend friend)
        {
            ICreateResponse response = await _esClient.CreateDocumentAsync<Friend>(friend);

            return response.Id;
        }

        public async Task<string> RemoveFriend(string id)
        {
            IDeleteResponse response = await _esClient.DeleteAsync<Friend>(id);

            return response.IsValid ? response.Id : null;
        }
    }
}
