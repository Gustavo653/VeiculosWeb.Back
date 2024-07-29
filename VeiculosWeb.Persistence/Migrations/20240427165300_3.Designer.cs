﻿// <auto-generated />
using System;
using VeiculosWeb.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace VeiculosWeb.Persistence.Migrations
{
    [DbContext(typeof(VeiculosWebContext))]
    [Migration("20240427165300_3")]
    partial class _3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("VeiculosWeb.Domain.Base.Tenant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Tenants");
                });

            modelBuilder.Entity("VeiculosWeb.Domain.Base.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<int?>("Coren")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<Guid?>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.HasIndex("TenantId");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("VeiculosWeb.Domain.Driver.DriverCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("Name", "TenantId")
                        .IsUnique();

                    b.ToTable("DriverCategories");
                });

            modelBuilder.Entity("VeiculosWeb.Domain.Driver.DriverChecklist", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("RequireFullReview")
                        .HasColumnType("boolean");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("Name", "TenantId")
                        .IsUnique();

                    b.ToTable("DriverChecklists");
                });

            modelBuilder.Entity("VeiculosWeb.Domain.Driver.DriverChecklistCheckedItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ChecklistItemId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ChecklistReviewId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("DriverChecklistItemId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("DriverChecklistReviewId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("OptionId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("DriverChecklistItemId");

                    b.HasIndex("DriverChecklistReviewId");

                    b.HasIndex("OptionId");

                    b.HasIndex("ChecklistItemId", "ChecklistReviewId", "TenantId", "OptionId")
                        .IsUnique();

                    b.ToTable("DriverChecklistReplacedItems");
                });

            modelBuilder.Entity("VeiculosWeb.Domain.Driver.DriverChecklistItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("DriverCategoryId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("DriverChecklistId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("DriverItemId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("DriverCategoryId");

                    b.HasIndex("DriverChecklistId");

                    b.HasIndex("DriverItemId", "DriverCategoryId", "DriverChecklistId", "TenantId")
                        .IsUnique();

                    b.ToTable("DriverChecklistItems");
                });

            modelBuilder.Entity("VeiculosWeb.Domain.Driver.DriverChecklistReview", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AmbulanceId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("DriverChecklistId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsFullReview")
                        .HasColumnType("boolean");

                    b.Property<string>("Observation")
                        .HasColumnType("text");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AmbulanceId");

                    b.HasIndex("DriverChecklistId");

                    b.HasIndex("UserId");

                    b.ToTable("DriverChecklistReviews");
                });

            modelBuilder.Entity("VeiculosWeb.Domain.Driver.DriverItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("Name", "TenantId")
                        .IsUnique();

                    b.ToTable("DriverItems");
                });

            modelBuilder.Entity("VeiculosWeb.Domain.Driver.DriverMedia", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("DriverChecklistCheckedItemId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DriverChecklistCheckedItemId");

                    b.ToTable("DriverMedias");
                });

            modelBuilder.Entity("VeiculosWeb.Domain.Driver.DriverOption", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("DriverChecklistItemId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("RequireSomeAction")
                        .HasColumnType("boolean");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("DriverChecklistItemId");

                    b.HasIndex("Name", "TenantId")
                        .IsUnique();

                    b.ToTable("DriverOptions");
                });

            modelBuilder.Entity("VeiculosWeb.Domain.Paramedic.ParamedicCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("Name", "TenantId")
                        .IsUnique();

                    b.ToTable("ParamedicCategories");
                });

            modelBuilder.Entity("VeiculosWeb.Domain.Paramedic.ParamedicChecklist", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("RequireFullReview")
                        .HasColumnType("boolean");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("Name", "TenantId")
                        .IsUnique();

                    b.ToTable("ParamedicChecklists");
                });

            modelBuilder.Entity("VeiculosWeb.Domain.Paramedic.ParamedicChecklistItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("ParamedicCategoryId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ParamedicChecklistId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ParamedicItemId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ParamedicParentChecklistItemId")
                        .HasColumnType("uuid");

                    b.Property<int>("RequiredQuantity")
                        .HasColumnType("integer");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("ParamedicCategoryId");

                    b.HasIndex("ParamedicChecklistId");

                    b.HasIndex("ParamedicParentChecklistItemId");

                    b.HasIndex("ParamedicItemId", "ParamedicCategoryId", "ParamedicChecklistId", "TenantId", "ParamedicParentChecklistItemId")
                        .IsUnique();

                    b.ToTable("ParamedicChecklistItems");
                });

            modelBuilder.Entity("VeiculosWeb.Domain.Paramedic.ParamedicChecklistReplacedItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("ParamedicChecklistItemId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ParamedicChecklistReviewId")
                        .HasColumnType("uuid");

                    b.Property<int>("ReplacedQuantity")
                        .HasColumnType("integer");

                    b.Property<int>("ReplenishmentQuantity")
                        .HasColumnType("integer");

                    b.Property<int>("RequiredQuantity")
                        .HasColumnType("integer");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("ParamedicChecklistReviewId");

                    b.HasIndex("ParamedicChecklistItemId", "ParamedicChecklistReviewId", "TenantId")
                        .IsUnique();

                    b.ToTable("ParamedicChecklistReplacedItems");
                });

            modelBuilder.Entity("VeiculosWeb.Domain.Paramedic.ParamedicChecklistReview", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AmbulanceId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsFullReview")
                        .HasColumnType("boolean");

                    b.Property<string>("Observation")
                        .HasColumnType("text");

                    b.Property<Guid>("ParamedicChecklistId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AmbulanceId");

                    b.HasIndex("ParamedicChecklistId");

                    b.HasIndex("UserId");

                    b.ToTable("ParamedicChecklistReviews");
                });

            modelBuilder.Entity("VeiculosWeb.Domain.Paramedic.ParamedicItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("Name", "TenantId")
                        .IsUnique();

                    b.ToTable("ParamedicItems");
                });

            modelBuilder.Entity("VeiculosWeb.Domain.Shared.Ambulance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("LicensePlate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("Number", "LicensePlate", "TenantId")
                        .IsUnique();

                    b.ToTable("Ambulances");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("VeiculosWeb.Domain.Base.User", b =>
                {
                    b.HasOne("VeiculosWeb.Domain.Base.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId");

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("VeiculosWeb.Domain.Driver.DriverChecklistCheckedItem", b =>
                {
                    b.HasOne("VeiculosWeb.Domain.Driver.DriverChecklistItem", "DriverChecklistItem")
                        .WithMany()
                        .HasForeignKey("DriverChecklistItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VeiculosWeb.Domain.Driver.DriverChecklistReview", "DriverChecklistReview")
                        .WithMany("DriverChecklistCheckedItems")
                        .HasForeignKey("DriverChecklistReviewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VeiculosWeb.Domain.Driver.DriverOption", "Option")
                        .WithMany("DriverChecklistCheckedItems")
                        .HasForeignKey("OptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DriverChecklistItem");

                    b.Navigation("DriverChecklistReview");

                    b.Navigation("Option");
                });

            modelBuilder.Entity("VeiculosWeb.Domain.Driver.DriverChecklistItem", b =>
                {
                    b.HasOne("VeiculosWeb.Domain.Driver.DriverCategory", "DriverCategory")
                        .WithMany()
                        .HasForeignKey("DriverCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VeiculosWeb.Domain.Driver.DriverChecklist", "DriverChecklist")
                        .WithMany("DriverChecklistItems")
                        .HasForeignKey("DriverChecklistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VeiculosWeb.Domain.Driver.DriverItem", "DriverItem")
                        .WithMany()
                        .HasForeignKey("DriverItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DriverCategory");

                    b.Navigation("DriverChecklist");

                    b.Navigation("DriverItem");
                });

            modelBuilder.Entity("VeiculosWeb.Domain.Driver.DriverChecklistReview", b =>
                {
                    b.HasOne("VeiculosWeb.Domain.Shared.Ambulance", "Ambulance")
                        .WithMany()
                        .HasForeignKey("AmbulanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VeiculosWeb.Domain.Driver.DriverChecklist", "DriverChecklist")
                        .WithMany()
                        .HasForeignKey("DriverChecklistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VeiculosWeb.Domain.Base.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Ambulance");

                    b.Navigation("DriverChecklist");

                    b.Navigation("User");
                });

            modelBuilder.Entity("VeiculosWeb.Domain.Driver.DriverMedia", b =>
                {
                    b.HasOne("VeiculosWeb.Domain.Driver.DriverChecklistCheckedItem", "DriverChecklistCheckedItem")
                        .WithMany("Medias")
                        .HasForeignKey("DriverChecklistCheckedItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DriverChecklistCheckedItem");
                });

            modelBuilder.Entity("VeiculosWeb.Domain.Driver.DriverOption", b =>
                {
                    b.HasOne("VeiculosWeb.Domain.Driver.DriverChecklistItem", "DriverChecklistItem")
                        .WithMany("DriverOptions")
                        .HasForeignKey("DriverChecklistItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DriverChecklistItem");
                });

            modelBuilder.Entity("VeiculosWeb.Domain.Paramedic.ParamedicChecklistItem", b =>
                {
                    b.HasOne("VeiculosWeb.Domain.Paramedic.ParamedicCategory", "ParamedicCategory")
                        .WithMany()
                        .HasForeignKey("ParamedicCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VeiculosWeb.Domain.Paramedic.ParamedicChecklist", "ParamedicChecklist")
                        .WithMany("ParamedicChecklistItems")
                        .HasForeignKey("ParamedicChecklistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VeiculosWeb.Domain.Paramedic.ParamedicItem", "ParamedicItem")
                        .WithMany()
                        .HasForeignKey("ParamedicItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VeiculosWeb.Domain.Paramedic.ParamedicChecklistItem", "ParamedicParentChecklistItem")
                        .WithMany("ParamedicChecklistSubItems")
                        .HasForeignKey("ParamedicParentChecklistItemId");

                    b.Navigation("ParamedicCategory");

                    b.Navigation("ParamedicChecklist");

                    b.Navigation("ParamedicItem");

                    b.Navigation("ParamedicParentChecklistItem");
                });

            modelBuilder.Entity("VeiculosWeb.Domain.Paramedic.ParamedicChecklistReplacedItem", b =>
                {
                    b.HasOne("VeiculosWeb.Domain.Paramedic.ParamedicChecklistItem", "ParamedicChecklistItem")
                        .WithMany("ParamedicChecklistReplacedItems")
                        .HasForeignKey("ParamedicChecklistItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VeiculosWeb.Domain.Paramedic.ParamedicChecklistReview", "ParamedicChecklistReview")
                        .WithMany("ParamedicChecklistReplacedItems")
                        .HasForeignKey("ParamedicChecklistReviewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParamedicChecklistItem");

                    b.Navigation("ParamedicChecklistReview");
                });

            modelBuilder.Entity("VeiculosWeb.Domain.Paramedic.ParamedicChecklistReview", b =>
                {
                    b.HasOne("VeiculosWeb.Domain.Shared.Ambulance", "Ambulance")
                        .WithMany()
                        .HasForeignKey("AmbulanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VeiculosWeb.Domain.Paramedic.ParamedicChecklist", "ParamedicChecklist")
                        .WithMany()
                        .HasForeignKey("ParamedicChecklistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VeiculosWeb.Domain.Base.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Ambulance");

                    b.Navigation("ParamedicChecklist");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("VeiculosWeb.Domain.Base.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("VeiculosWeb.Domain.Base.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VeiculosWeb.Domain.Base.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("VeiculosWeb.Domain.Base.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VeiculosWeb.Domain.Driver.DriverChecklist", b =>
                {
                    b.Navigation("DriverChecklistItems");
                });

            modelBuilder.Entity("VeiculosWeb.Domain.Driver.DriverChecklistCheckedItem", b =>
                {
                    b.Navigation("Medias");
                });

            modelBuilder.Entity("VeiculosWeb.Domain.Driver.DriverChecklistItem", b =>
                {
                    b.Navigation("DriverOptions");
                });

            modelBuilder.Entity("VeiculosWeb.Domain.Driver.DriverChecklistReview", b =>
                {
                    b.Navigation("DriverChecklistCheckedItems");
                });

            modelBuilder.Entity("VeiculosWeb.Domain.Driver.DriverOption", b =>
                {
                    b.Navigation("DriverChecklistCheckedItems");
                });

            modelBuilder.Entity("VeiculosWeb.Domain.Paramedic.ParamedicChecklist", b =>
                {
                    b.Navigation("ParamedicChecklistItems");
                });

            modelBuilder.Entity("VeiculosWeb.Domain.Paramedic.ParamedicChecklistItem", b =>
                {
                    b.Navigation("ParamedicChecklistReplacedItems");

                    b.Navigation("ParamedicChecklistSubItems");
                });

            modelBuilder.Entity("VeiculosWeb.Domain.Paramedic.ParamedicChecklistReview", b =>
                {
                    b.Navigation("ParamedicChecklistReplacedItems");
                });
#pragma warning restore 612, 618
        }
    }
}
