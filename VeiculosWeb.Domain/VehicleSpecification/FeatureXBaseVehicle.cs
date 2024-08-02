using VeiculosWeb.Domain.Base;
using VeiculosWeb.Domain.Vehicles;

namespace VeiculosWeb.Domain.VehicleSpecification;

public class FeatureXBaseVehicle : BaseEntity
{
    public Guid BaseVehicleId { get; set; }
    public required virtual BaseVehicle BaseVehicle { get; set; }
    public Guid FeatureId { get; set; }
    public required virtual Feature Feature { get; set; }
}