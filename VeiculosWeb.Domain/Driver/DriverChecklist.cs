using VeiculosWeb.Domain.Shared;

namespace VeiculosWeb.Domain.Driver
{
    public class DriverChecklist : BasicEntity
    {
        public required bool RequireFullReview { get; set; }
        public virtual List<DriverChecklistItem>? DriverChecklistItems { get; set; }
    }
}
