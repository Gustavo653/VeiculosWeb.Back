using VeiculosWeb.Domain.Shared;

namespace VeiculosWeb.Domain.Driver
{
    public class DriverOption : BasicEntity
    {
        public required bool RequireSomeAction { get; set; }
        public virtual required DriverChecklistItem DriverChecklistItem { get; set; }
        public virtual List<DriverChecklistCheckedItem>? DriverChecklistCheckedItems { get; set; }
    }
}
