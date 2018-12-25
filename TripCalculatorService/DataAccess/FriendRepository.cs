using Elasticsearch.Net;
using Nest;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripCalculatorService.Entities;

namespace TripCalculatorService.DataAccess
{
    public interface IFriendRepository
    {
        Task <IEnumerable <Friend> > GetAll();
        Task <Friend> Get(string id);
        Task <Friend> AddFriend(Friend friend);
        Task <IDeleteResponse> RemoveFriend(string id);
        Task <bool> AddPurchasedItem(string friendId, PurchasedItem item);
        Task <bool> RemovePurchasedItem(string friendId, PurchasedItem item);
    }
    public class FriendRepository : IFriendRepository
    {
        private IElasticClient _esClient;

        public FriendRepository(IElasticClient esClient)
        {
            _esClient = esClient;
        }

        public async Task <IEnumerable <Friend> > GetAll()
        {
            var result = await _esClient.SearchAsync <Friend>(s => s);

            return(result.Documents);
        }

        public async Task <Friend> Get(string id) { return((await _esClient.GetAsync <Friend>(id)).Source); }

        public async Task <bool> AddPurchasedItem(string friendId, PurchasedItem item)
        {
            Friend friend = await this.Get(friendId);

            IUpdateRequest <Friend, Friend> updateRequest = new UpdateRequest <Friend, Friend>("friends", "friend", friendId);

            friend.PurchasedItems.Add(item);

            updateRequest.Doc = friend;

            var response = await _esClient.UpdateAsync <Friend>(updateRequest);

            return(response.ServerError == null || response.ServerError.Error == null);
        }

        public async Task <bool> RemovePurchasedItem(string friendId, PurchasedItem item)
        {
            Friend friend = await this.Get(friendId);

            IUpdateRequest <Friend, Friend> updateRequest = new UpdateRequest <Friend, Friend>("friends", "friend", friendId);

            friend.PurchasedItems.Remove(item);

            updateRequest.Doc = friend;

            var response = await _esClient.UpdateAsync <Friend>(updateRequest);

            return(response.ServerError == null || response.ServerError.Error == null);
        }

        public async Task <IIndexResponse> AddFriend(Friend friend) { return(await _esClient.IndexAsync <Friend>(friend, f => f.Index("friends").Type("friend"))); }

        public async Task <IDeleteResponse> RemoveFriend(string id)
        {
            var deleteRequest = new DeleteRequest("friends", "friend", id);

            return(await _esClient.DeleteAsync(deleteRequest));
        }
    }
}
