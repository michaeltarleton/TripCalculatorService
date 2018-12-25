using Elasticsearch.Net;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public async Task<DataAccessResponse<IEnumerable<Friend>>> GetAll()
        {
            ISearchResponse<Friend> response = await _esClient.SearchAsync<Friend>(s => s);

            if (!response.IsValid) return DataAccessResponse.InternalServerError<IEnumerable<Friend>>();

            IEnumerable<Friend> friends = response.Hits.Select(h => {
                var source = h.Source;
                source.Id  = h.Id;
                return source;
            });

            if (friends == null) return DataAccessResponse.NotFound<IEnumerable<Friend>>();
            if (friends.Count() == 0) return DataAccessResponse.NotFound<IEnumerable<Friend>>();

            return DataAccessResponse.OK(friends);
        }

        public async Task<DataAccessResponse<Friend>> Get(string id)
        {
            IGetResponse<Friend> response = await _esClient.GetAsync<Friend>(id);

            if (!response.IsValid) return DataAccessResponse.InternalServerError<Friend>();

            Friend friend = response.Source;

            if (friend == null) return DataAccessResponse.NotFound<Friend>();

            friend.Id = response.Id;

            return DataAccessResponse.OK(friend);;
        }

        public async Task<DataAccessResponse<string>> AddFriend(Friend friend)
        {
            if (friend == null) return DataAccessResponse.NotFound<string>();

            if (friend.PurchasedItems != null) friend.PurchasedItems.ForEach(p => p.Id = Guid.NewGuid());

            IIndexResponse response = await _esClient.IndexAsync<Friend>(friend, d => d.Index(_index).Type(_type));

            if (!response.IsValid) return DataAccessResponse.InternalServerError<string>();

            return DataAccessResponse.Created(response.Id);
        }

        public async Task<DataAccessResponse<string>> UpdateFriend(string id, Friend friend)
        {
            if (friend == null) return DataAccessResponse.NotFound<string>();

            if (friend.PurchasedItems != null) friend.PurchasedItems.ForEach(p => p.Id = Guid.NewGuid());

            IUpdateResponse<Friend> response = await _esClient.UpdateAsync<Friend>(friend, d => d.Index(_index).Type(_type).Doc(friend));

            if (!response.IsValid) return DataAccessResponse.InternalServerError<string>();

            return DataAccessResponse.NoContent(response.Id);
        }

        public async Task<DataAccessResponse<string>> RemoveFriend(string id)
        {
            if (id == null) return DataAccessResponse.NotFound<string>();

            IDeleteResponse response = await _esClient.DeleteAsync<Friend>(id);

            if (!response.IsValid) return DataAccessResponse.InternalServerError<string>();
            if (response.Id == null) return DataAccessResponse.InternalServerError<string>();

            return DataAccessResponse.NoContent(response.Id);
        }

        public async Task<DataAccessResponse<string>> AddPurchasedItem(string friendId, PurchasedItem item)
        {
            if (friendId == null) return DataAccessResponse.NotFound<string>();
            if (item == null) return DataAccessResponse.NotFound<string>();

            var friendResponse = await this.Get(friendId);

            if (friendResponse.Status != HttpStatusCode.OK) return DataAccessResponse.NotFound<string>();

            Friend friend = friendResponse.Payload;

            item.Id = Guid.NewGuid();

            friend.PurchasedItems = friend.PurchasedItems == null ? new List<PurchasedItem>() : friend.PurchasedItems;

            friend.PurchasedItems.Add(item);

            IUpdateResponse<Friend> response = await _esClient.UpdateAsync<Friend>(friendId, d => d.Index(_index).Type(_type).Doc(friend));

            if (!response.IsValid) return DataAccessResponse.InternalServerError<string>();
            if (response.Id == null) return DataAccessResponse.InternalServerError<string>();

            return DataAccessResponse.NoContent(response.Id);
        }

        public async Task<DataAccessResponse<string>> RemovePurchasedItem(string friendId, Guid purchasedItemId)
        {
            if (friendId == null) return DataAccessResponse.NotFound<string>();
            if (purchasedItemId == null) return DataAccessResponse.NotFound<string>();

            var friendResponse = await this.Get(friendId);

            if (friendResponse.Status != HttpStatusCode.OK) return DataAccessResponse.NotFound<string>();

            Friend friend = friendResponse.Payload;

            if (friend.PurchasedItems == null) return DataAccessResponse.BadRequest<string>();

            PurchasedItem purchasedItemToRemove = friend.PurchasedItems.FirstOrDefault(p => p.Id == purchasedItemId);

            if (purchasedItemToRemove == null) return DataAccessResponse.NotFound<string>();

            friend.PurchasedItems.Remove(purchasedItemToRemove);

            IUpdateResponse<Friend> response = await _esClient.UpdateAsync<Friend>(friendId, d => d.Index(_index).Type(_type).Doc(friend));

            if (!response.IsValid) return DataAccessResponse.InternalServerError<string>();
            if (response.Id == null) return DataAccessResponse.InternalServerError<string>();

            return DataAccessResponse.NoContent(response.Id);
        }

        public async Task<DataAccessResponse<string>> UpdatePurchasedItem(string friendId, Guid purchasedItemId, PurchasedItem purchasedItem)
        {
            if (friendId == null) return DataAccessResponse.NotFound<string>();
            if (purchasedItemId == null) return DataAccessResponse.NotFound<string>();

            var friendResponse = await this.Get(friendId);

            if (friendResponse.Status != HttpStatusCode.OK) return DataAccessResponse.NotFound<string>();

            Friend friend = friendResponse.Payload;

            if (friend.PurchasedItems == null) return DataAccessResponse.BadRequest<string>();

            PurchasedItem purchasedItemToUpdate = friend.PurchasedItems.FirstOrDefault(p => p.Id == purchasedItemId);

            if (purchasedItemToUpdate == null) return DataAccessResponse.NotFound<string>();

            purchasedItem.Id = purchasedItemId;

            friend.PurchasedItems.Remove(purchasedItemToUpdate);
            friend.PurchasedItems.Add(purchasedItem);

            IUpdateResponse<Friend> response = await _esClient.UpdateAsync<Friend>(friendId, d => d.Index(_index).Type(_type).Doc(friend));

            if (!response.IsValid) return DataAccessResponse.InternalServerError<string>();
            if (response.Id == null) return DataAccessResponse.InternalServerError<string>();

            return DataAccessResponse.NoContent(response.Id);
        }
    }
}
