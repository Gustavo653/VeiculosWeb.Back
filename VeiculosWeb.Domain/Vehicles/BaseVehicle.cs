using VeiculosWeb.Domain.Base;
using VeiculosWeb.Domain.CarSpecification;
using VeiculosWeb.Domain.Location;

namespace VeiculosWeb.Domain.Vehicles
{
    public class BaseVehicle : BaseEntity
    {
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
        public IList<Feature>? Features { get; set; }
        public IList<Image>? Images { get; set; }
    }
}
