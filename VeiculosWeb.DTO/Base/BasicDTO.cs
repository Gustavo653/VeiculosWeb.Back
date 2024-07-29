using System.ComponentModel.DataAnnotations;

namespace VeiculosWeb.DTO.Base
{
    public class BasicDTO
    {
        [Required] public required string Name { get; set; }
    }
}