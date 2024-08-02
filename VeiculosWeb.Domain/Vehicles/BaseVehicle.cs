using VeiculosWeb.Domain.Base;
using VeiculosWeb.Domain.Location;
using VeiculosWeb.Domain.VehicleSpecification;

namespace VeiculosWeb.Domain.Vehicles
{
    public abstract class BaseVehicle : BaseEntity
    {
        public required User User { get; set; }
        public required Brand Brand { get; set; }
        public required Model Model { get; set; }
        public float? Mileage { get; set; }
        public DateTime? YearOfManufacture { get; set; }
        public DateTime? YearOfModel { get; set; }
        public Fuel? Fuel { get; set; }
        public Gearbox? Gearbox { get; set; }
        public Color? Color { get; set; }
        public State? State { get; set; }
        public City? City { get; set; }
        public virtual IList<FeatureXBaseVehicle>? FeatureXBaseVehicles { get; set; }
        public virtual IList<Image>? Images { get; set; }
    }
}
