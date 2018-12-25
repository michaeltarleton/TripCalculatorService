using System;
using System.Collections.Generic;
using System.Linq;

namespace TripCalculatorService.Models
{
    public class Friend
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<PurchasedItem> PurchasedItems { get; set; }
        public decimal TotalAmountPaid => this.PurchasedItems.Sum(i => i.Price);

        public Friend()
        {
            PurchasedItems = new List<PurchasedItem>();
        }
    }
}
