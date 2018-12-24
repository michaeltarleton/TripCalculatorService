using System.Collections.Generic;
using System.Linq;

namespace TripCalculatorService.Models
{
    public class Friend
    {
        public string Name;
        public readonly List <PurchaseItem> AmountsPaid;
        public decimal TotalAmountPaid => this.AmountsPaid.Sum(i => i.Price);

        public Friend()
        {
            AmountsPaid = new List <PurchaseItem> ();
        }
    }
}
