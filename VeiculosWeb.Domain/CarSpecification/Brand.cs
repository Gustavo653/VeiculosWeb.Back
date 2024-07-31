using VeiculosWeb.Domain.Shared;

namespace VeiculosWeb.Domain.CarSpecification
{
    public class Brand : BasicEntity
    {
        public required int Code { get; set; }
        public virtual List<Model>? Models { get; set; }
    }
}
