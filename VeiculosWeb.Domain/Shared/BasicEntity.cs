namespace VeiculosWeb.Domain.Shared
{
    public abstract class BasicEntity : TenantBaseEntity
    {
        public required string Name { get; set; }
    }
}
