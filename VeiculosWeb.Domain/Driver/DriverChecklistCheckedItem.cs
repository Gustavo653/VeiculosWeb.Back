using VeiculosWeb.Domain.Shared;

namespace VeiculosWeb.Domain.Driver
{
    public class DriverChecklistCheckedItem : TenantBaseEntity
    {
        public Guid DriverOptionId { get; set; }
        public virtual required DriverOption DriverOption { get; set; }
        public virtual List<DriverMedia>? DriverMedias { get; set; }
        public Guid DriverChecklistReviewId { get; set; }
        public virtual required DriverChecklistReview DriverChecklistReview { get; set; }
        public Guid DriverChecklistItemId { get; set; }
        public virtual required DriverChecklistItem DriverChecklistItem { get; set; }
    }
}
