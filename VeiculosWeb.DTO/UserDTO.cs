using System.ComponentModel.DataAnnotations;
using VeiculosWeb.Domain.Enum;

namespace VeiculosWeb.DTO
{
    public class UserDTO
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required RoleName Role { get; set; }
    }
}