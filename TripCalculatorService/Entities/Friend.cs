using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TripCalculatorService.Entities
{
    public class Friend : BaseEntity, IEquatable <Friend>
    {
        public string Name { get; set; }

        public List <PurchaseItem> PurchasedItems { get; set; }

        public static Friend BuildRandomFriend(int seed = 0)
        {
            Random random = new Random(seed);

            string[]       names = { "Sam", "Fred", "James", "John", "Sarah", "Jill" };
            PurchaseItem[] items =
            {
                new PurchaseItem { Name = "Chips",     Price = 8.00M },
                new PurchaseItem { Name = "Cheese",    Price = 8.00M },
                new PurchaseItem { Name = "Soda",      Price = 8.00M },
                new PurchaseItem { Name = "Chocolate", Price = 8.00M }
            };

            string       randomName     = names[random.Next(0, names.Length - 1)];
            PurchaseItem randomPurchase = items[random.Next(0, items.Length - 1)];

            Friend friend = new Friend {
                Name           = randomName,
                PurchasedItems = new List <PurchaseItem> {
                    randomPurchase
                }
            };

            return(friend);
        }

        public static IEnumerable <Friend> BuildRandomFriends(int maxFrineds = 10)
        {
            Friend[] friends = new Friend[maxFrineds];

            for (int i = 0; i < maxFrineds; i++)
            {
                friends[i] = Friend.BuildRandomFriend(i);
            }

            var distinct = (friends.Distinct());

            return(distinct);
        }

        public bool Equals(Friend other) { return(this.GetHashCode() == other.GetHashCode()); }

        public override int GetHashCode() { return(Name.GetHashCode()); }
    }
}
