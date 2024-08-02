using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeiculosWeb.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Features_BaseVehicle_BaseVehicleId",
                table: "Features");

            migrationBuilder.DropIndex(
                name: "IX_Features_BaseVehicleId",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "BaseVehicleId",
                table: "Features");

            migrationBuilder.CreateTable(
                name: "FeatureXBaseVehicles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BaseVehicleId = table.Column<Guid>(type: "uuid", nullable: false),
                    FeatureId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureXBaseVehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeatureXBaseVehicles_BaseVehicle_BaseVehicleId",
                        column: x => x.BaseVehicleId,
                        principalTable: "BaseVehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeatureXBaseVehicles_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeatureXBaseVehicles_BaseVehicleId",
                table: "FeatureXBaseVehicles",
                column: "BaseVehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_FeatureXBaseVehicles_FeatureId_BaseVehicleId",
                table: "FeatureXBaseVehicles",
                columns: new[] { "FeatureId", "BaseVehicleId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeatureXBaseVehicles");

            migrationBuilder.AddColumn<Guid>(
                name: "BaseVehicleId",
                table: "Features",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Features_BaseVehicleId",
                table: "Features",
                column: "BaseVehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Features_BaseVehicle_BaseVehicleId",
                table: "Features",
                column: "BaseVehicleId",
                principalTable: "BaseVehicle",
                principalColumn: "Id");
        }
    }
}
