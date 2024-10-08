﻿using VeiculosWeb.Domain.Enum;
using VeiculosWeb.Domain.Shared;

namespace VeiculosWeb.Domain.VehicleSpecification
{
    public class Model : BasicEntity
    {
        public required VehicleType VehicleType { get; set; }
        public required int Code { get; set; }
        public Guid BrandId { get; set; }
        public virtual required Brand Brand { get; set; }
    }
}
