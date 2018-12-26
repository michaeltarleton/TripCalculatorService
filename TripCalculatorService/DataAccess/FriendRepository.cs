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
            DataAccessResponse<IEnumerable<Friend>> response = new DataAccessResponse<IEnumerable<Friend>>();

            ISearchResponse<Friend> searchResponse = await _esClient.SearchAsync<Friend>(s => s);

            if (!searchResponse.IsValid) return response.InternalServerError();

            IEnumerable<Friend> friends = searchResponse.Hits.Select(h => {
                var source = h.Source;
                source.Id  = h.Id;
                return source;
            });

            if (friends == null) return response.NotFound();
            if (friends.Count() == 0) return response.NotFound();

            return response.OK(friends);
        }

        public async Task<DataAccessResponse<Friend>> Get(string id)
        {
            DataAccessResponse<Friend> response = new DataAccessResponse<Friend>();

            IGetResponse<Friend> getResponse = await _esClient.GetAsync<Friend>(id);

            if (!getResponse.IsValid) return response.InternalServerError();

            Friend friend = getResponse.Source;

            if (friend == null) return response.NotFound();

            friend.Id = getResponse.Id;

            return response.OK(friend);;
        }

        public async Task<DataAccessResponse<string>> AddFriend(Friend friend)
        {
            DataAccessResponse<string> response = new DataAccessResponse<string>();

            if (friend == null) return response.NotFound();

            if (friend.PurchasedItems != null) friend.PurchasedItems.ForEach(p => p.Id = Guid.NewGuid());

            IIndexResponse indexResponse = await _esClient.IndexAsync<Friend>(friend, d => d.Index(_index).Type(_type));

            if (!indexResponse.IsValid) return response.InternalServerError();

            return response.Created(indexResponse.Id);
        }

        public async Task<DataAccessResponse<string>> UpdateFriend(string id, Friend friend)
        {
            DataAccessResponse<string> response = new DataAccessResponse<string>();

            if (friend == null) return response.NotFound();

            if (friend.PurchasedItems != null) friend.PurchasedItems.ForEach(p => p.Id = Guid.NewGuid());

            IUpdateResponse<Friend> updateResponse = await _esClient.UpdateAsync<Friend>(friend,
                                                                                         d => d
                                                                                             .Index(_index)
                                                                                             .Type(_type)
                                                                                             .Doc(friend));

            if (!updateResponse.IsValid) return response.InternalServerError();

            return response.NoContent(updateResponse.Id);
        }

        public async Task<DataAccessResponse<string>> RemoveFriend(string id)
        {
            DataAccessResponse<string> response = new DataAccessResponse<string>();

            if (id == null) return response.NotFound();

            IDeleteResponse deleteResponse = await _esClient.DeleteAsync<Friend>(id);

            if (!deleteResponse.IsValid) return response.InternalServerError();
            if (deleteResponse.Id == null) return response.InternalServerError();

            return response.NoContent(deleteResponse.Id);
        }

        public async Task<DataAccessResponse<string>> AddPurchasedItem(string friendId, PurchasedItem item)
        {
            DataAccessResponse<string> response = new DataAccessResponse<string>();

            if (friendId == null) return response.NotFound();
            if (item == null) return response.NotFound();

            var friendResponse = await this.Get(friendId);

            if (friendResponse.Status != HttpStatusCode.OK) return response.NotFound();

            Friend friend = friendResponse.Payload;

            item.Id = Guid.NewGuid();

            friend.PurchasedItems = friend.PurchasedItems == null ? new List<PurchasedItem>() : friend.PurchasedItems;

            friend.PurchasedItems.Add(item);

            IUpdateResponse<Friend> updateResponse = await _esClient.UpdateAsync<Friend>(friendId,
                                                                                         d => d
                                                                                             .Index(_index)
                                                                                             .Type(_type)
                                                                                             .Doc(friend));

            if (!updateResponse.IsValid) return response.InternalServerError();
            if (updateResponse.Id == null) return response.InternalServerError();

            return response.NoContent(updateResponse.Id);
        }

        public async Task<DataAccessResponse<string>> RemovePurchasedItem(string friendId, Guid purchasedItemId)
        {
            DataAccessResponse<string> response = new DataAccessResponse<string>();

            if (friendId == null) return response.NotFound();
            if (purchasedItemId == null) return response.NotFound();

            var friendResponse = await this.Get(friendId);

            if (friendResponse.Status != HttpStatusCode.OK) return response.NotFound();

            Friend friend = friendResponse.Payload;

            if (friend.PurchasedItems == null) return response.BadRequest();

            PurchasedItem purchasedItemToRemove = friend.PurchasedItems.FirstOrDefault(p => p.Id == purchasedItemId);

            if (purchasedItemToRemove == null) return response.NotFound();

            friend.PurchasedItems.Remove(purchasedItemToRemove);

            IUpdateResponse<Friend> updateResponse = await _esClient.UpdateAsync<Friend>(friendId,
                                                                                         d => d
                                                                                             .Index(_index)
                                                                                             .Type(_type)
                                                                                             .Doc(friend));

            if (!updateResponse.IsValid) return response.InternalServerError();
            if (updateResponse.Id == null) return response.InternalServerError();

            return response.NoContent(updateResponse.Id);
        }

        public async Task<DataAccessResponse<string>> UpdatePurchasedItem(string friendId, Guid purchasedItemId, PurchasedItem purchasedItem)
        {
            DataAccessResponse<string> response = new DataAccessResponse<string>();

            if (friendId == null) return response.NotFound();
            if (purchasedItemId == null) return response.NotFound();

            var friendResponse = await this.Get(friendId);

            if (friendResponse.Status != HttpStatusCode.OK) return response.NotFound();

            Friend friend = friendResponse.Payload;

            if (friend.PurchasedItems == null) return response.BadRequest();

            PurchasedItem purchasedItemToUpdate = friend.PurchasedItems.FirstOrDefault(p => p.Id == purchasedItemId);

            if (purchasedItemToUpdate == null) return response.NotFound();

            purchasedItem.Id = purchasedItemId;

            friend.PurchasedItems.Remove(purchasedItemToUpdate);
            friend.PurchasedItems.Add(purchasedItem);

            IUpdateResponse<Friend> updateResponse = await _esClient.UpdateAsync<Friend>(friendId,
                                                                                         d => d
                                                                                             .Index(_index)
                                                                                             .Type(_type)
                                                                                             .Doc(friend));

            if (!updateResponse.IsValid) return response.InternalServerError();
            if (updateResponse.Id == null) return response.InternalServerError();

            return response.NoContent(updateResponse.Id);
        }

        public async Task<DataAccessResponse<PurchasedItem>> GetPurchasedItem(string friendId, Guid purchasedItemId)
        {
            DataAccessResponse<PurchasedItem> response = new DataAccessResponse<PurchasedItem>();

            if (friendId == null) return response.NotFound();
            if (purchasedItemId == null) return response.NotFound();

            var friendResponse = await this.Get(friendId);

            if (friendResponse.Status != HttpStatusCode.OK) return response.NotFound();

            Friend friend = friendResponse.Payload;

            if (friend.PurchasedItems == null) return response.BadRequest();

            PurchasedItem purchasedItem = friend.PurchasedItems.FirstOrDefault(p => p.Id == purchasedItemId);

            if (purchasedItem == null) return response.NotFound();

            purchasedItem.Id = purchasedItemId;

            return response.OK(purchasedItem);
        }

        public async Task<DataAccessResponse<IEnumerable<PurchasedItem>>> GetAllPurchasedItems(string friendId)
        {
            DataAccessResponse<IEnumerable<PurchasedItem>> response = new DataAccessResponse<IEnumerable<PurchasedItem>>();

            if (friendId == null) return response.NotFound();

            var friendResponse = await this.Get(friendId);

            if (friendResponse.Status != HttpStatusCode.OK) return response.NotFound();

            Friend friend = friendResponse.Payload;

            if (friend.PurchasedItems == null) return response.NotFound();

            return response.NoContent(friendResponse.Payload.PurchasedItems);
        }
    }
}
