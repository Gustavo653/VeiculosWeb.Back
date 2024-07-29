using VeiculosWeb.Domain.Paramedic;
using VeiculosWeb.Infrastructure.Repository.Paramedic;
using VeiculosWeb.Persistence;
using Microsoft.AspNetCore.Http;

namespace VeiculosWeb.DataAccess.Paramedic
{
    public class ParamedicChecklistReplacedItemRepository(IHttpContextAccessor httpContextAccessor, VeiculosWebContext context) :
                 TenantBaseRepository<ParamedicChecklistReplacedItem, VeiculosWebContext>(httpContextAccessor, context), IParamedicChecklistReplacedItemRepository
    {
    }
}
