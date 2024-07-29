using VeiculosWeb.Domain.Base;
using VeiculosWeb.Domain.Shared;

namespace VeiculosWeb.Domain.Driver
{
    public class DriverChecklistReview : TenantBaseEntity
    {
        public required bool IsFullReview { get; set; }
        public string? Observation { get; set; }
        public virtual required Ambulance Ambulance { get; set; }
        public virtual required DriverChecklist DriverChecklist { get; set; }
        public virtual required User User { get; set; }
        public virtual List<DriverChecklistCheckedItem>? DriverChecklistCheckedItems { get; set; }
    }
}
