using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeiculosWeb.Domain.Base;
using VeiculosWeb.Domain.Location;
using VeiculosWeb.Domain.VehicleSpecification;

namespace VeiculosWeb.DTO
{
    public class BaseVehicleDTO
    {
        public required Guid BrandId { get; set; }
        public required Guid ModelId { get; set; }
        public float? Odometer { get; set; }
        public DateTime? YearOfManufacture { get; set; }
        public DateTime? YearOfModel { get; set; }
        public Guid? FuelId { get; set; }
        public Guid? GearboxId { get; set; }
        public Guid? ColorId { get; set; }
        public Guid? StateId { get; set; }
        public Guid? CityId { get; set; }
        public IList<Guid>? Features { get; set; }
    }
}
