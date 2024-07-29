using VeiculosWeb.Domain.Enum;
using Microsoft.AspNetCore.Identity;

namespace VeiculosWeb.Domain.Base
{
    public class User : IdentityUser
    {
        public required string Name { get; set; }
        public int? Coren { get; set; }
        public Guid? TenantId { get; set; }
        public Tenant? Tenant { get; set; }
        public required RoleName Role { get; set; }
    }
}