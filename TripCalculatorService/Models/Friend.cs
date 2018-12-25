using System.Collections.Generic;
using System.Linq;

namespace TripCalculatorService.Models
{
    public class Friend
    {
        public string Name;
        public List <PurchaseItem> PurchasedItems;
        public decimal TotalAmountPaid => this.PurchasedItems.Sum(i => i.Price);

        public Friend()
        {
            PurchasedItems = new List <PurchaseItem> ();
        }
    }
}
