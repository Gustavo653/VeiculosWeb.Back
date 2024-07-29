using VeiculosWeb.Domain.Base;
using VeiculosWeb.Infrastructure.Repository;
using VeiculosWeb.Persistence;

namespace VeiculosWeb.DataAccess
{
    public class TenantRepository : BaseRepository<Tenant, VeiculosWebContext>, ITenantRepository
    {
        public TenantRepository(VeiculosWebContext context) : base(context)
        {
        }
    }
}
