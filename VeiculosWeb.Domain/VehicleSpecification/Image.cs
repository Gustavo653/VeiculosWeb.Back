using VeiculosWeb.Domain.Base;

namespace VeiculosWeb.Domain.CarSpecification
{
    public class Image : BaseEntity
    {
        public required string Url { get; set; }
    }
}
