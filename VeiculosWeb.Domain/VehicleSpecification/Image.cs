using VeiculosWeb.Domain.Base;
using VeiculosWeb.Domain.Vehicles;

namespace VeiculosWeb.Domain.VehicleSpecification
{
    public class Image : BaseEntity
    {
        public required string Url { get; set; }
        public virtual required BaseVehicle BaseVehicle { get; set; }
    }
}
