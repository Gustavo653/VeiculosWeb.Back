using VeiculosWeb.Domain.Shared;
using VeiculosWeb.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace VeiculosWeb.DataAccess
{
    public abstract class TenantBaseRepository<TType, TContext>(IHttpContextAccessor httpContextAccessor, TContext context) : BaseRepository<TType, TContext>(context)
        where TType : TenantBaseEntity
        where TContext : DbContext
    {
        private ISession Session => httpContextAccessor.HttpContext!.Session;

        public override Task InsertAsync(TType entity)
        {
            Session.TryGetValue(Consts.ClaimTenantId, out byte[]? tenantId);
            entity.TenantId = Guid.Parse(Encoding.UTF8.GetString(tenantId!));
            entity.SetCreatedAt();
            return base.InsertAsync(entity);
        }

        public override void Update(TType entity)
        {
            entity.SetUpdatedAt();
            base.Update(entity);
        }
    }
}