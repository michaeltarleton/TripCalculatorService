using System;

namespace TripCalculatorService.Models
{
    public class PurchasedItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
