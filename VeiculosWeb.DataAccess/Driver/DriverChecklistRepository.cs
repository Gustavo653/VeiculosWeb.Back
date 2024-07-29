using VeiculosWeb.Domain.Driver;
using VeiculosWeb.Infrastructure.Repository.Driver;
using VeiculosWeb.Persistence;
using Microsoft.AspNetCore.Http;

namespace VeiculosWeb.DataAccess.Driver
{
    public class DriverChecklistRepository(IHttpContextAccessor httpContextAccessor, VeiculosWebContext context) :
                 TenantBaseRepository<DriverChecklist, VeiculosWebContext>(httpContextAccessor, context), IDriverChecklistRepository
    {
    }
}
