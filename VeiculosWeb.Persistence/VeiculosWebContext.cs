using VeiculosWeb.Domain.Base;
using VeiculosWeb.Domain.Shared;
using VeiculosWeb.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Text;
using VeiculosWeb.Domain.CarSpecification;

namespace VeiculosWeb.Persistence
{
    public class VeiculosWebContext(DbContextOptions<VeiculosWebContext> options, IHttpContextAccessor httpContextAccessor) : IdentityDbContext<User>(options)
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public DbSet<Fuel> Fuels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Fuel>(x =>
            {
                x.HasKey(a => a.Id);
                x.HasIndex(a => new { a.Name }).IsUnique();
            });
        }
    }
}