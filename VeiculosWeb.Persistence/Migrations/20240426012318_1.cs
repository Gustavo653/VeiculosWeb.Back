using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace VeiculosWeb.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ambulances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    LicensePlate = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ambulances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DriverCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DriverChecklists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RequireFullReview = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverChecklists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DriverItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParamedicCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParamedicCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParamedicChecklists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RequireFullReview = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParamedicChecklists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParamedicItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParamedicItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DriverChecklistItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChecklistId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentChecklistItemId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverChecklistItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriverChecklistItems_DriverCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "DriverCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriverChecklistItems_DriverChecklistItems_ParentChecklistIt~",
                        column: x => x.ParentChecklistItemId,
                        principalTable: "DriverChecklistItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DriverChecklistItems_DriverChecklists_ChecklistId",
                        column: x => x.ChecklistId,
                        principalTable: "DriverChecklists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriverChecklistItems_DriverItems_ItemId",
                        column: x => x.ItemId,
                        principalTable: "DriverItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParamedicChecklistItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RequiredQuantity = table.Column<int>(type: "integer", nullable: false),
                    ItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChecklistId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentChecklistItemId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParamedicChecklistItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParamedicChecklistItems_ParamedicCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ParamedicCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParamedicChecklistItems_ParamedicChecklistItems_ParentCheck~",
                        column: x => x.ParentChecklistItemId,
                        principalTable: "ParamedicChecklistItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ParamedicChecklistItems_ParamedicChecklists_ChecklistId",
                        column: x => x.ChecklistId,
                        principalTable: "ParamedicChecklists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParamedicChecklistItems_ParamedicItems_ItemId",
                        column: x => x.ItemId,
                        principalTable: "ParamedicItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Coren = table.Column<int>(type: "integer", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DriverOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RequireSomeAction = table.Column<bool>(type: "boolean", nullable: false),
                    ChecklistItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriverOptions_DriverChecklistItems_ChecklistItemId",
                        column: x => x.ChecklistItemId,
                        principalTable: "DriverChecklistItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DriverChecklistReviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsFullReview = table.Column<bool>(type: "boolean", nullable: false),
                    Observation = table.Column<string>(type: "text", nullable: true),
                    AmbulanceId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChecklistId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverChecklistReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriverChecklistReviews_Ambulances_AmbulanceId",
                        column: x => x.AmbulanceId,
                        principalTable: "Ambulances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriverChecklistReviews_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DriverChecklistReviews_DriverChecklists_ChecklistId",
                        column: x => x.ChecklistId,
                        principalTable: "DriverChecklists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParamedicChecklistReviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsFullReview = table.Column<bool>(type: "boolean", nullable: false),
                    Observation = table.Column<string>(type: "text", nullable: true),
                    AmbulanceId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChecklistId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParamedicChecklistReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParamedicChecklistReviews_Ambulances_AmbulanceId",
                        column: x => x.AmbulanceId,
                        principalTable: "Ambulances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParamedicChecklistReviews_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ParamedicChecklistReviews_ParamedicChecklists_ChecklistId",
                        column: x => x.ChecklistId,
                        principalTable: "ParamedicChecklists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DriverChecklistReplacedItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OptionId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChecklistReviewId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChecklistItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverChecklistReplacedItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriverChecklistReplacedItems_DriverChecklistItems_Checklist~",
                        column: x => x.ChecklistItemId,
                        principalTable: "DriverChecklistItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriverChecklistReplacedItems_DriverChecklistReviews_Checkli~",
                        column: x => x.ChecklistReviewId,
                        principalTable: "DriverChecklistReviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriverChecklistReplacedItems_DriverOptions_OptionId",
                        column: x => x.OptionId,
                        principalTable: "DriverOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParamedicChecklistReplacedItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReplacedQuantity = table.Column<int>(type: "integer", nullable: false),
                    RequiredQuantity = table.Column<int>(type: "integer", nullable: false),
                    ReplenishmentQuantity = table.Column<int>(type: "integer", nullable: false),
                    ChecklistReviewId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChecklistItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParamedicChecklistReplacedItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParamedicChecklistReplacedItems_ParamedicChecklistItems_Che~",
                        column: x => x.ChecklistItemId,
                        principalTable: "ParamedicChecklistItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParamedicChecklistReplacedItems_ParamedicChecklistReviews_C~",
                        column: x => x.ChecklistReviewId,
                        principalTable: "ParamedicChecklistReviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DriverMedias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    ChecklistCheckedItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverMedias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriverMedias_DriverChecklistReplacedItems_ChecklistCheckedI~",
                        column: x => x.ChecklistCheckedItemId,
                        principalTable: "DriverChecklistReplacedItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ambulances_Number_LicensePlate_TenantId",
                table: "Ambulances",
                columns: new[] { "Number", "LicensePlate", "TenantId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TenantId",
                table: "AspNetUsers",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DriverCategories_Name_TenantId",
                table: "DriverCategories",
                columns: new[] { "Name", "TenantId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DriverChecklistItems_CategoryId",
                table: "DriverChecklistItems",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverChecklistItems_ChecklistId",
                table: "DriverChecklistItems",
                column: "ChecklistId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverChecklistItems_ItemId_CategoryId_ChecklistId_TenantId~",
                table: "DriverChecklistItems",
                columns: new[] { "ItemId", "CategoryId", "ChecklistId", "TenantId", "ParentChecklistItemId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DriverChecklistItems_ParentChecklistItemId",
                table: "DriverChecklistItems",
                column: "ParentChecklistItemId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverChecklistReplacedItems_ChecklistItemId_ChecklistRevie~",
                table: "DriverChecklistReplacedItems",
                columns: new[] { "ChecklistItemId", "ChecklistReviewId", "TenantId", "OptionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DriverChecklistReplacedItems_ChecklistReviewId",
                table: "DriverChecklistReplacedItems",
                column: "ChecklistReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverChecklistReplacedItems_OptionId",
                table: "DriverChecklistReplacedItems",
                column: "OptionId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverChecklistReviews_AmbulanceId",
                table: "DriverChecklistReviews",
                column: "AmbulanceId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverChecklistReviews_ChecklistId",
                table: "DriverChecklistReviews",
                column: "ChecklistId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverChecklistReviews_UserId",
                table: "DriverChecklistReviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverChecklists_Name_TenantId",
                table: "DriverChecklists",
                columns: new[] { "Name", "TenantId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DriverItems_Name_TenantId",
                table: "DriverItems",
                columns: new[] { "Name", "TenantId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DriverMedias_ChecklistCheckedItemId",
                table: "DriverMedias",
                column: "ChecklistCheckedItemId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverOptions_ChecklistItemId",
                table: "DriverOptions",
                column: "ChecklistItemId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverOptions_Name_TenantId",
                table: "DriverOptions",
                columns: new[] { "Name", "TenantId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParamedicCategories_Name_TenantId",
                table: "ParamedicCategories",
                columns: new[] { "Name", "TenantId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParamedicChecklistItems_CategoryId",
                table: "ParamedicChecklistItems",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ParamedicChecklistItems_ChecklistId",
                table: "ParamedicChecklistItems",
                column: "ChecklistId");

            migrationBuilder.CreateIndex(
                name: "IX_ParamedicChecklistItems_ItemId_CategoryId_ChecklistId_Tenan~",
                table: "ParamedicChecklistItems",
                columns: new[] { "ItemId", "CategoryId", "ChecklistId", "TenantId", "ParentChecklistItemId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParamedicChecklistItems_ParentChecklistItemId",
                table: "ParamedicChecklistItems",
                column: "ParentChecklistItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ParamedicChecklistReplacedItems_ChecklistItemId_ChecklistRe~",
                table: "ParamedicChecklistReplacedItems",
                columns: new[] { "ChecklistItemId", "ChecklistReviewId", "TenantId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParamedicChecklistReplacedItems_ChecklistReviewId",
                table: "ParamedicChecklistReplacedItems",
                column: "ChecklistReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_ParamedicChecklistReviews_AmbulanceId",
                table: "ParamedicChecklistReviews",
                column: "AmbulanceId");

            migrationBuilder.CreateIndex(
                name: "IX_ParamedicChecklistReviews_ChecklistId",
                table: "ParamedicChecklistReviews",
                column: "ChecklistId");

            migrationBuilder.CreateIndex(
                name: "IX_ParamedicChecklistReviews_UserId",
                table: "ParamedicChecklistReviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ParamedicChecklists_Name_TenantId",
                table: "ParamedicChecklists",
                columns: new[] { "Name", "TenantId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParamedicItems_Name_TenantId",
                table: "ParamedicItems",
                columns: new[] { "Name", "TenantId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_Name",
                table: "Tenants",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DriverMedias");

            migrationBuilder.DropTable(
                name: "ParamedicChecklistReplacedItems");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "DriverChecklistReplacedItems");

            migrationBuilder.DropTable(
                name: "ParamedicChecklistItems");

            migrationBuilder.DropTable(
                name: "ParamedicChecklistReviews");

            migrationBuilder.DropTable(
                name: "DriverChecklistReviews");

            migrationBuilder.DropTable(
                name: "DriverOptions");

            migrationBuilder.DropTable(
                name: "ParamedicCategories");

            migrationBuilder.DropTable(
                name: "ParamedicItems");

            migrationBuilder.DropTable(
                name: "ParamedicChecklists");

            migrationBuilder.DropTable(
                name: "Ambulances");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "DriverChecklistItems");

            migrationBuilder.DropTable(
                name: "Tenants");

            migrationBuilder.DropTable(
                name: "DriverCategories");

            migrationBuilder.DropTable(
                name: "DriverChecklists");

            migrationBuilder.DropTable(
                name: "DriverItems");
        }
    }
}
