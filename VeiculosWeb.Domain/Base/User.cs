using VeiculosWeb.Domain.Enum;
using Microsoft.AspNetCore.Identity;

namespace VeiculosWeb.Domain.Base
{
    public class User : IdentityUser
    {
        public required string Name { get; set; }
        public required RoleName Role { get; set; }
    }
}