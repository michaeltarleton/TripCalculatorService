namespace TripCalculatorService.Entities {
    public abstract class BaseEntity : AuditableEntity {
        public string Id { get; set; }
    }
}