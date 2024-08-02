using System.ComponentModel.DataAnnotations;

namespace VeiculosWeb.DTO
{
    public class UserEmailDTO
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        public required string Code { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$", ErrorMessage = "A senha deve ter no mínimo 6 caracteres, contendo letras e números.")]
        public required string Password { get; set; }
    }
}