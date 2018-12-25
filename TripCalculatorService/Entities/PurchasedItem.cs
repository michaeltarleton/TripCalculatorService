using System;

namespace TripCalculatorService.Entities
{
    public class PurchasedItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public PurchasedItem()
        {
            Id = Guid.NewGuid();
        }
    }
}
