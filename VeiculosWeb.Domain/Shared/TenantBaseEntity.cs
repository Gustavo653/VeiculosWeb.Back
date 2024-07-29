using VeiculosWeb.Domain.Base;

namespace VeiculosWeb.Domain.Shared
{
    public abstract class TenantBaseEntity : BaseEntity
    {
        public Guid TenantId { get; set; }
    }
}
