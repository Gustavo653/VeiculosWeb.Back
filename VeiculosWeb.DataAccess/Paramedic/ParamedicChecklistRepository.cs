using VeiculosWeb.Domain.Paramedic;
using VeiculosWeb.Infrastructure.Repository.Paramedic;
using VeiculosWeb.Persistence;
using Microsoft.AspNetCore.Http;

namespace VeiculosWeb.DataAccess.Paramedic
{
    public class ParamedicChecklistRepository(IHttpContextAccessor httpContextAccessor, VeiculosWebContext context) :
                 TenantBaseRepository<ParamedicChecklist, VeiculosWebContext>(httpContextAccessor, context), IParamedicChecklistRepository
    {
    }
}
