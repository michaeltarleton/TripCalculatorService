using System.Collections.Generic;

namespace TripCalculatorService.Entities {
    public class Friend : BaseEntity {
        string Name;

        public readonly List<PurchaseItem> AmountsPaid;
    }
}