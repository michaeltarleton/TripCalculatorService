using System.Collections.Generic;

namespace TripCalculatorService.Entities
{
    public class Friend : BaseEntity
    {
        public string Name { get; set; }

        public List <PurchaseItem> AmountsPaid { get; set; }
    }
}
