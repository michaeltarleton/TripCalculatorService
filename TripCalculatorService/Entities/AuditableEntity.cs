using System;

namespace TripCalculatorService.Entities {
    public abstract class AuditableEntity {
        DateTime DateCreated { get; set; }
        DateTime DateModified { get; set; }
        string CreatedBy { get; set; }
        string ModifiedBy { get; set; }
    }
}