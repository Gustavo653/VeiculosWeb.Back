using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VeiculosWeb.Domain.Base;
using VeiculosWeb.Domain.CarSpecification;

namespace VeiculosWeb.Persistence
{
    public class VeiculosWebContext(DbContextOptions<VeiculosWebContext> options) : IdentityDbContext<User>(options)
    {
        public DbSet<Fuel> Fuels { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Fuel>(x =>
            {
                x.HasIndex(a => new { a.Name }).IsUnique();
            });

            modelBuilder.Entity<Brand>(x =>
            {
                x.HasIndex(a => new { a.Code }).IsUnique();
            });

            modelBuilder.Entity<Model>(x =>
            {
                x.HasIndex(a => new { a.Code, a.BrandId }).IsUnique();
            });
        }
    }
}