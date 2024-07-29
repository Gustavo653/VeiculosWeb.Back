using VeiculosWeb.Domain.Base;
using VeiculosWeb.Domain.Driver;
using VeiculosWeb.Domain.Paramedic;
using VeiculosWeb.Domain.Shared;
using VeiculosWeb.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace VeiculosWeb.Persistence
{
    public class VeiculosWebContext(DbContextOptions<VeiculosWebContext> options, IHttpContextAccessor httpContextAccessor) : IdentityDbContext<User>(options)
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private ISession Session => _httpContextAccessor.HttpContext!.Session;

        public DbSet<Ambulance> Ambulances { get; set; }
        public DbSet<Tenant> Tenants { get; set; }

        public DbSet<DriverCategory> DriverCategories { get; set; }
        public DbSet<DriverChecklist> DriverChecklists { get; set; }
        public DbSet<DriverChecklistCheckedItem> DriverChecklistCheckedItems { get; set; }
        public DbSet<DriverChecklistItem> DriverChecklistItems { get; set; }
        public DbSet<DriverChecklistReview> DriverChecklistReviews { get; set; }
        public DbSet<DriverItem> DriverItems { get; set; }
        public DbSet<DriverMedia> DriverMedias { get; set; }
        public DbSet<DriverOption> DriverOptions { get; set; }

        public DbSet<ParamedicCategory> ParamedicCategories { get; set; }
        public DbSet<ParamedicChecklist> ParamedicChecklists { get; set; }
        public DbSet<ParamedicChecklistItem> ParamedicChecklistItems { get; set; }
        public DbSet<ParamedicChecklistReplacedItem> ParamedicChecklistReplacedItems { get; set; }
        public DbSet<ParamedicChecklistReview> ParamedicChecklistReviews { get; set; }
        public DbSet<ParamedicItem> ParamedicItems { get; set; }

        private Guid? GetTenantId()
        {
            Session.TryGetValue(Consts.ClaimTenantId, out byte[]? tenantId);

            if (Guid.TryParse(Encoding.UTF8.GetString(tenantId!), out Guid userId))
                return userId;

            return null;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ambulance>(x =>
            {
                x.HasKey(a => a.Id);
                x.HasIndex(a => new { a.Number, a.LicensePlate, a.TenantId }).IsUnique();
                x.HasQueryFilter(a => a.TenantId == (GetTenantId() ?? a.TenantId));
            });

            modelBuilder.Entity<Tenant>(x =>
            {
                x.HasKey(a => a.Id);
                x.HasIndex(a => a.Name).IsUnique();
            });

            ModelBuilderDriver(modelBuilder);

            ModelBuilderParamedic(modelBuilder);
        }

        private void ModelBuilderParamedic(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ParamedicCategory>(x =>
            {
                x.HasKey(a => a.Id);
                x.HasIndex(a => new { a.Name, a.TenantId }).IsUnique();
                x.HasQueryFilter(a => a.TenantId == (GetTenantId() ?? a.TenantId));
            });

            modelBuilder.Entity<ParamedicChecklist>(x =>
            {
                x.HasKey(a => a.Id);
                x.HasIndex(a => new { a.Name, a.TenantId }).IsUnique();
                x.HasQueryFilter(a => a.TenantId == (GetTenantId() ?? a.TenantId));
            });

            modelBuilder.Entity<ParamedicChecklistItem>(x =>
            {
                x.HasKey(a => a.Id);
                x.HasIndex(a => new
                {
                    ItemId = a.ParamedicItemId,
                    CategoryId = a.ParamedicCategoryId,
                    ChecklistId = a.ParamedicChecklistId,
                    a.TenantId,
                    ParentChecklistItemId = a.ParamedicParentChecklistItemId
                }).IsUnique();
                x.HasQueryFilter(a => a.TenantId == (GetTenantId() ?? a.TenantId));
            });

            modelBuilder.Entity<ParamedicChecklistReplacedItem>(x =>
            {
                x.HasKey(a => a.Id);
                x.HasIndex(a => new { ChecklistItemId = a.ParamedicChecklistItemId, ChecklistReviewId = a.ParamedicChecklistReviewId, a.TenantId }).IsUnique();
                x.HasQueryFilter(a => a.TenantId == (GetTenantId() ?? a.TenantId));
            });

            modelBuilder.Entity<ParamedicChecklistReview>(x =>
            {
                x.HasKey(a => a.Id);
                x.HasQueryFilter(a => a.TenantId == (GetTenantId() ?? a.TenantId));
            });

            modelBuilder.Entity<ParamedicItem>(x =>
            {
                x.HasKey(a => a.Id);
                x.HasIndex(a => new { a.Name, a.TenantId }).IsUnique();
                x.HasQueryFilter(a => a.TenantId == (GetTenantId() ?? a.TenantId));
            });
        }

        private void ModelBuilderDriver(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DriverCategory>(x =>
            {
                x.HasKey(a => a.Id);
                x.HasIndex(a => new { a.Name, a.TenantId }).IsUnique();
                x.HasQueryFilter(a => a.TenantId == (GetTenantId() ?? a.TenantId));
            });

            modelBuilder.Entity<DriverChecklist>(x =>
            {
                x.HasKey(a => a.Id);
                x.HasIndex(a => new { a.Name, a.TenantId }).IsUnique();
                x.HasQueryFilter(a => a.TenantId == (GetTenantId() ?? a.TenantId));
            });

            modelBuilder.Entity<DriverChecklistItem>(x =>
            {
                x.HasKey(a => a.Id);
                x.HasIndex(a => new { ItemId = a.DriverItemId, CategoryId = a.DriverCategoryId, ChecklistId = a.DriverChecklistId, a.TenantId }).IsUnique();
                x.HasQueryFilter(a => a.TenantId == (GetTenantId() ?? a.TenantId));
            });

            modelBuilder.Entity<DriverChecklistCheckedItem>(x =>
            {
                x.HasKey(a => a.Id);
                x.HasIndex(a => new { a.DriverChecklistItemId, a.DriverChecklistReviewId, a.TenantId, a.DriverOptionId }).IsUnique();
                x.HasQueryFilter(a => a.TenantId == (GetTenantId() ?? a.TenantId));
            });

            modelBuilder.Entity<DriverChecklistReview>(x =>
            {
                x.HasKey(a => a.Id);
                x.HasQueryFilter(a => a.TenantId == (GetTenantId() ?? a.TenantId));
            });

            modelBuilder.Entity<DriverItem>(x =>
            {
                x.HasKey(a => a.Id);
                x.HasIndex(a => new { a.Name, a.TenantId }).IsUnique();
                x.HasQueryFilter(a => a.TenantId == (GetTenantId() ?? a.TenantId));
            });

            modelBuilder.Entity<DriverMedia>(x =>
            {
                x.HasKey(a => a.Id);
                x.HasQueryFilter(a => a.TenantId == (GetTenantId() ?? a.TenantId));
            });

            modelBuilder.Entity<DriverOption>(x =>
            {
                x.HasKey(a => a.Id);
                x.HasIndex(a => new { a.Name, a.TenantId }).IsUnique();
                x.HasQueryFilter(a => a.TenantId == (GetTenantId() ?? a.TenantId));
            });
        }
    }
}