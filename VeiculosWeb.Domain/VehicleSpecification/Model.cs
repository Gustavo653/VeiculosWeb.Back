using VeiculosWeb.Domain.Enum;
using VeiculosWeb.Domain.Shared;

namespace VeiculosWeb.Domain.CarSpecification
{
    public class Model : BasicEntity
    {
        public required VehicleType VehicleType { get; set; }
        public required int Code { get; set; }
        public Guid BrandId { get; set; }
        public required Brand Brand { get; set; }
    }
}
