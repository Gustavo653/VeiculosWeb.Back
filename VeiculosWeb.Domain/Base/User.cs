using Microsoft.AspNetCore.Identity;
using VeiculosWeb.Domain.Enum;

namespace VeiculosWeb.Domain.Base
{
    public class User : IdentityUser
    {
        public required string Name { get; set; }
        public required RoleName Role { get; set; }
    }
}