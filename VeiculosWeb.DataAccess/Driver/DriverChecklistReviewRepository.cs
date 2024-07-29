using VeiculosWeb.Domain.Driver;
using VeiculosWeb.Infrastructure.Repository.Driver;
using VeiculosWeb.Persistence;
using Microsoft.AspNetCore.Http;

namespace VeiculosWeb.DataAccess.Driver
{
    public class DriverChecklistReviewRepository(IHttpContextAccessor httpContextAccessor, VeiculosWebContext context) :
                 TenantBaseRepository<DriverChecklistReview, VeiculosWebContext>(httpContextAccessor, context), IDriverChecklistReviewRepository
    {
    }
}
