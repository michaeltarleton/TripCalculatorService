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
        public static readonly string Index = "friends";
        public static readonly string Type  = "friend";

        public FriendRepository(IElasticClient esClient)
        {
            _esClient = esClient;
        }

        public async Task<DataAccessResponse<IEnumerable<Friend>>> GetAll()
        {
            DataAccessResponse<IEnumerable<Friend>> response = new DataAccessResponse<IEnumerable<Friend>>();

            ISearchResponse<Friend> searchResponse = await _esClient.SearchAsync<Friend>(s => s);

            if (searchResponse.Total == 0) return response.NotFound();

            if (!searchResponse.IsValid) return response.InternalServerError();

            IEnumerable<Friend> friends = searchResponse.Hits.Select(h => {
                Friend source = h.Source;
                source.Id     = h.Id;
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

            if (!getResponse.Found) return response.NotFound();

            if (!getResponse.IsValid) return response.InternalServerError();

            Friend friend = getResponse.Source;

            friend.Id = getResponse.Id;

            return response.OK(friend);;
        }

        public async Task<DataAccessResponse<string>> AddFriend(Friend friend)
        {
            DataAccessResponse<string> response = new DataAccessResponse<string>();

            if (friend == null) return response.NotFound();

            if (friend.PurchasedItems != null) friend.PurchasedItems.ForEach(p => p.Id = Guid.NewGuid());

            IIndexResponse indexResponse = await _esClient.IndexAsync<Friend>(friend, d => d.Index(Index).Type(Type));

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
                                                                                             .Index(Index)
                                                                                             .Type(Type)
                                                                                             .Doc(friend));

            if (!updateResponse.IsValid) return response.InternalServerError();

            return response.NoContent(updateResponse.Id);
        }

        public async Task<DataAccessResponse<string>> RemoveFriend(string id)
        {
            DataAccessResponse<string> response = new DataAccessResponse<string>();

            if (id == null) return response.NotFound();

            IDeleteResponse deleteResponse = await _esClient.DeleteAsync<Friend>(id);

            if (deleteResponse.Result == Result.NotFound) return response.NotFound();

            if (!deleteResponse.IsValid) return response.InternalServerError();
            if (deleteResponse.Id == null) return response.InternalServerError();

            return response.NoContent(deleteResponse.Id);
        }
    }
}
