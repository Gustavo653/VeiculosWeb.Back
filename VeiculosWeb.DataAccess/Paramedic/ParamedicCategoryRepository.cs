using VeiculosWeb.Domain.Paramedic;
using VeiculosWeb.Infrastructure.Repository.Paramedic;
using VeiculosWeb.Persistence;
using Microsoft.AspNetCore.Http;

namespace VeiculosWeb.DataAccess.Paramedic
{
    public class ParamedicCategoryRepository(IHttpContextAccessor httpContextAccessor, VeiculosWebContext context) :
                 TenantBaseRepository<ParamedicCategory, VeiculosWebContext>(httpContextAccessor, context), IParamedicCategoryRepository
    {
    }
}
