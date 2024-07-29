using VeiculosWeb.Domain.Shared;
using VeiculosWeb.Infrastructure.Repository;
using VeiculosWeb.Persistence;
using Microsoft.AspNetCore.Http;

namespace VeiculosWeb.DataAccess
{
    public class AmbulanceRepository : TenantBaseRepository<Ambulance, VeiculosWebContext>, IAmbulanceRepository
    {
        public AmbulanceRepository(IHttpContextAccessor httpContextAccessor, VeiculosWebContext context) : base(httpContextAccessor, context)
        {
        }
    }
}
