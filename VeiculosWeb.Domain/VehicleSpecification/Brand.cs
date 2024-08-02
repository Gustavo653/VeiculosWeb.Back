using VeiculosWeb.Domain.Enum;
using VeiculosWeb.Domain.Shared;

namespace VeiculosWeb.Domain.VehicleSpecification
{
    public class Brand : BasicEntity
    {
        public required VehicleType VehicleType { get; set; }
        public required int Code { get; set; }
        public virtual List<Model>? Models { get; set; }
    }
}
