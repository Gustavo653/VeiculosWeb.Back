using System.ComponentModel.DataAnnotations;
using VeiculosWeb.Domain.Enum;
using VeiculosWeb.DTO.Base;

namespace VeiculosWeb.DTO
{
    public class FeatureDTO : BasicDTO
    {        
        [Required]
        public required VehicleType VehicleType { get; set; }
    }
}