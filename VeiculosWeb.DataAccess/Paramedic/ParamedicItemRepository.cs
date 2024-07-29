using VeiculosWeb.Domain.Paramedic;
using VeiculosWeb.Infrastructure.Repository.Paramedic;
using VeiculosWeb.Persistence;
using Microsoft.AspNetCore.Http;

namespace VeiculosWeb.DataAccess.Paramedic
{
    public class ParamedicItemRepository(IHttpContextAccessor httpContextAccessor, VeiculosWebContext context) :
                 TenantBaseRepository<ParamedicItem, VeiculosWebContext>(httpContextAccessor, context), IParamedicItemRepository
    {
    }
}
