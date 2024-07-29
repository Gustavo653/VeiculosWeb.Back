using VeiculosWeb.Domain.Paramedic;
using VeiculosWeb.Infrastructure.Repository.Paramedic;
using VeiculosWeb.Persistence;
using Microsoft.AspNetCore.Http;

namespace VeiculosWeb.DataAccess.Paramedic
{
    public class ParamedicChecklistItemRepository(IHttpContextAccessor httpContextAccessor, VeiculosWebContext context) :
                 TenantBaseRepository<ParamedicChecklistItem, VeiculosWebContext>(httpContextAccessor, context), IParamedicChecklistItemRepository
    {
    }
}
