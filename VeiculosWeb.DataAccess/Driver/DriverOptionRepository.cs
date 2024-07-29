using VeiculosWeb.Domain.Driver;
using VeiculosWeb.Infrastructure.Repository.Driver;
using VeiculosWeb.Persistence;
using Microsoft.AspNetCore.Http;

namespace VeiculosWeb.DataAccess.Driver
{
    public class DriverOptionRepository(IHttpContextAccessor httpContextAccessor, VeiculosWebContext context) :
                 TenantBaseRepository<DriverOption, VeiculosWebContext>(httpContextAccessor, context), IDriverOptionRepository
    {
    }
}
