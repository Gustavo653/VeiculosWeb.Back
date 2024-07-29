using VeiculosWeb.Domain.Driver;
using VeiculosWeb.Infrastructure.Repository.Driver;
using VeiculosWeb.Persistence;
using Microsoft.AspNetCore.Http;

namespace VeiculosWeb.DataAccess.Driver
{
    public class DriverChecklistItemRepository(IHttpContextAccessor httpContextAccessor, VeiculosWebContext context) :
                 TenantBaseRepository<DriverChecklistItem, VeiculosWebContext>(httpContextAccessor, context), IDriverChecklistItemRepository
    {
    }
}
