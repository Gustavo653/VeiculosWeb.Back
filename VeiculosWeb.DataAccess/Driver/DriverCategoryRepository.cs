using VeiculosWeb.Domain.Driver;
using VeiculosWeb.Infrastructure.Repository.Driver;
using VeiculosWeb.Persistence;
using Microsoft.AspNetCore.Http;

namespace VeiculosWeb.DataAccess.Driver
{
    public class DriverCategoryRepository(IHttpContextAccessor httpContextAccessor, VeiculosWebContext context) :
                 TenantBaseRepository<DriverCategory, VeiculosWebContext>(httpContextAccessor, context), IDriverCategoryRepository
    {
    }
}
