using System;

namespace TripCalculatorService.Models
{
    public class PurchasedItem
    {
        public Guid Id { get; set; }
        public string Name;
        public decimal Price;
    }
}
