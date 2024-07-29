using VeiculosWeb.Domain.Shared;

namespace VeiculosWeb.Domain.Driver
{
    public class DriverChecklistItem : TenantBaseEntity
    {
        public Guid DriverItemId { get; set; }
        public virtual required DriverItem DriverItem { get; set; }
        public Guid DriverCategoryId { get; set; }
        public virtual required DriverCategory DriverCategory { get; set; }
        public Guid DriverChecklistId { get; set; }
        public virtual required DriverChecklist DriverChecklist { get; set; }
        public virtual required List<DriverOption> DriverOptions { get; set; }
        public virtual List<DriverChecklistCheckedItem>? DriverChecklistCheckedItems { get; set; }
    }
}
