﻿using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VeiculosWeb.Domain.Base;
using VeiculosWeb.Domain.Location;
using VeiculosWeb.Domain.Vehicles;
using VeiculosWeb.Domain.VehicleSpecification;

namespace VeiculosWeb.Persistence
{
    public class VeiculosWebContext(DbContextOptions<VeiculosWebContext> options) : IdentityDbContext<User>(options)
    {
        public DbSet<Fuel> Fuels { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Gearbox> Gearboxes { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<FeatureXBaseVehicle> FeatureXBaseVehicles { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Bike> Bikes { get; set; }

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

            modelBuilder.Entity<State>(x =>
            {
                x.HasIndex(a => new { a.Code }).IsUnique();
            });

            modelBuilder.Entity<City>(x =>
            {
                x.HasIndex(a => new { a.Code }).IsUnique();
            });

            modelBuilder.Entity<Color>(x =>
            {
                x.HasIndex(a => new { a.Name }).IsUnique();
            });

            modelBuilder.Entity<Feature>(x =>
            {
                x.HasIndex(a => new { a.Name, a.VehicleType }).IsUnique();
            });

            modelBuilder.Entity<Gearbox>(x =>
            {
                x.HasIndex(a => new { a.Name }).IsUnique();
            });

            modelBuilder.Entity<Image>(x =>
            {
                x.HasIndex(a => new { a.Url }).IsUnique();
            });
            
            modelBuilder.Entity<FeatureXBaseVehicle>(x =>
            {
                x.HasIndex(a => new { a.FeatureId, a.BaseVehicleId }).IsUnique();
            });
        }
    }
}