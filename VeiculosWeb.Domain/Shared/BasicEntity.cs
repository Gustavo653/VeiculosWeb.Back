using VeiculosWeb.Domain.Base;

namespace VeiculosWeb.Domain.Shared
{
    public abstract class BasicEntity : BaseEntity
    {
        public required string Name { get; set; }
    }
}
