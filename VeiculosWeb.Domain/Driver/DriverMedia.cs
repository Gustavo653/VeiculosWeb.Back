using VeiculosWeb.Domain.Shared;

namespace VeiculosWeb.Domain.Driver
{
    public class DriverMedia : TenantBaseEntity
    {
        public required string Url { get; set; }
        public required DriverChecklistCheckedItem DriverChecklistCheckedItem { get; set; }
    }
}
