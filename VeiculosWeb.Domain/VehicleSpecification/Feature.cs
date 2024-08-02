using VeiculosWeb.Domain.Enum;
using VeiculosWeb.Domain.Shared;

namespace VeiculosWeb.Domain.CarSpecification
{
    public class Feature : BasicEntity
    {
        public required VehicleType VehicleType { get; set; }
    }
}
