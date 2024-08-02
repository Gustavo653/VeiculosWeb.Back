using VeiculosWeb.Domain.Enum;
using VeiculosWeb.Domain.Shared;

namespace VeiculosWeb.Domain.VehicleSpecification
{
    public class Feature : BasicEntity
    {
        public required VehicleType VehicleType { get; set; }
        public virtual IList<FeatureXBaseVehicle>? FeatureXBaseVehicles { get; set; }
    }
}
