using VeiculosWeb.Domain.Shared;

namespace VeiculosWeb.Domain.CarSpecification
{
    public class Model : BasicEntity
    {
        public required int Code { get; set; }
        public required Guid BrandId { get; set; }
        public required Brand Brand { get; set; }
    }
}
