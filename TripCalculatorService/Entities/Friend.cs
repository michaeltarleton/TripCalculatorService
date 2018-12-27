using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TripCalculatorService.Entities
{
    public class Friend : BaseEntity, IEquatable<Friend>
    {
        public string Name { get; set; }

        public List<PurchasedItem> PurchasedItems { get; set; }

        public static Friend BuildRandomFriend(int seed = 0)
        {
            Random random = new Random(seed);

            string[]        names = { "Sam", "Fred", "James", "John", "Sarah", "Jill" };
            PurchasedItem[] items =
            {
                new PurchasedItem { Id = Guid.NewGuid(), Name = "Chips",     Price = 2.00M },
                new PurchasedItem { Id = Guid.NewGuid(), Name = "Cheese",    Price = 4.00M },
                new PurchasedItem { Id = Guid.NewGuid(), Name = "Soda",      Price = 6.00M },
                new PurchasedItem { Id = Guid.NewGuid(), Name = "Chocolate", Price = 8.00M }
            };

            string        randomName     = names[random.Next(0, names.Length - 1)];
            PurchasedItem randomPurchase = items[random.Next(0, items.Length - 1)];

            Friend friend = new Friend {
                Name           = randomName,
                PurchasedItems = new List<PurchasedItem> { randomPurchase }
            };

            return friend;
        }

        public static IEnumerable<Friend> BuildRandomFriends(int maxFrineds = 10)
        {
            Friend[] friends = new Friend[maxFrineds];

            for (int i = 0; i < maxFrineds; i++)
            {
                friends[i] = Friend.BuildRandomFriend(i);
            }

            IEnumerable<Friend> distinct = (friends.Distinct());

            return distinct;
        }

        public bool Equals(Friend other)
        {
            return this.GetHashCode() == other.GetHashCode();
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
