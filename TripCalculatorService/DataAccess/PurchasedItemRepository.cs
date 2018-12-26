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
    public class PurchasedItemRepository : IPurchasedItemRepository
    {
        private readonly IFriendRepository _friendRepo;
        private readonly IElasticClient _esClient;

        public PurchasedItemRepository(IFriendRepository friendRepo, IElasticClient esClient)
        {
            _friendRepo = friendRepo;
            _esClient   = esClient;
        }

        public async Task<DataAccessResponse<string>> AddPurchasedItem(string friendId, PurchasedItem item)
        {
            DataAccessResponse<string> response = new DataAccessResponse<string>();

            if (friendId == null) return response.NotFound();
            if (item == null) return response.NotFound();

            DataAccessResponse<Friend> friendResponse = await _friendRepo.Get(friendId);

            if (friendResponse.Status != HttpStatusCode.OK) return response.NotFound();

            Friend friend = friendResponse.Payload;

            item.Id = Guid.NewGuid();

            friend.PurchasedItems = friend.PurchasedItems == null ? new List<PurchasedItem>() : friend.PurchasedItems;

            friend.PurchasedItems.Add(item);

            IUpdateResponse<Friend> updateResponse = await _esClient.UpdateAsync<Friend>(friendId,
                                                                                         d => d
                                                                                             .Index(FriendRepository.Index)
                                                                                             .Type(FriendRepository.Type)
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

            DataAccessResponse<Friend> friendResponse = await _friendRepo.Get(friendId);

            if (friendResponse.Status != HttpStatusCode.OK) return response.NotFound();

            Friend friend = friendResponse.Payload;

            if (friend.PurchasedItems == null) return response.BadRequest();

            PurchasedItem purchasedItemToRemove = friend.PurchasedItems.FirstOrDefault(p => p.Id == purchasedItemId);

            if (purchasedItemToRemove == null) return response.NotFound();

            friend.PurchasedItems.Remove(purchasedItemToRemove);

            IUpdateResponse<Friend> updateResponse = await _esClient.UpdateAsync<Friend>(friendId,
                                                                                         d => d
                                                                                             .Index(FriendRepository.Index)
                                                                                             .Type(FriendRepository.Type)
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

            DataAccessResponse<Friend> friendResponse = await _friendRepo.Get(friendId);

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
                                                                                             .Index(FriendRepository.Index)
                                                                                             .Type(FriendRepository.Type)
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

            DataAccessResponse<Friend> friendResponse = await _friendRepo.Get(friendId);

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

            DataAccessResponse<Friend> friendResponse = await _friendRepo.Get(friendId);

            if (friendResponse.Status != HttpStatusCode.OK) return response.NotFound();

            Friend friend = friendResponse.Payload;

            if (friend.PurchasedItems == null) return response.NotFound();

            return response.NoContent(friendResponse.Payload.PurchasedItems);
        }
    }
}
