using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
