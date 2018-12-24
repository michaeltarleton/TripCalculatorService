namespace TripCalculatorService.Entities {
    public abstract class BaseEntity : AuditableEntity {
        string Id { get; set; }
    }
}