using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeiculosWeb.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BaseVehicleId",
                table: "Images",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BaseVehicleId",
                table: "Features",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BaseVehicle",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    BrandId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModelId = table.Column<Guid>(type: "uuid", nullable: false),
                    Mileage = table.Column<float>(type: "real", nullable: true),
                    YearOfManufacture = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    YearOfModel = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    FuelId = table.Column<Guid>(type: "uuid", nullable: true),
                    GearboxId = table.Column<Guid>(type: "uuid", nullable: true),
                    ColorId = table.Column<Guid>(type: "uuid", nullable: true),
                    StateId = table.Column<Guid>(type: "uuid", nullable: true),
                    CityId = table.Column<Guid>(type: "uuid", nullable: true),
                    Discriminator = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseVehicle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaseVehicle_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BaseVehicle_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseVehicle_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BaseVehicle_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BaseVehicle_Fuels_FuelId",
                        column: x => x.FuelId,
                        principalTable: "Fuels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BaseVehicle_Gearboxes_GearboxId",
                        column: x => x.GearboxId,
                        principalTable: "Gearboxes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BaseVehicle_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseVehicle_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_BaseVehicleId",
                table: "Images",
                column: "BaseVehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Features_BaseVehicleId",
                table: "Features",
                column: "BaseVehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseVehicle_BrandId",
                table: "BaseVehicle",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseVehicle_CityId",
                table: "BaseVehicle",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseVehicle_ColorId",
                table: "BaseVehicle",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseVehicle_FuelId",
                table: "BaseVehicle",
                column: "FuelId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseVehicle_GearboxId",
                table: "BaseVehicle",
                column: "GearboxId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseVehicle_ModelId",
                table: "BaseVehicle",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseVehicle_StateId",
                table: "BaseVehicle",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseVehicle_UserId",
                table: "BaseVehicle",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Features_BaseVehicle_BaseVehicleId",
                table: "Features",
                column: "BaseVehicleId",
                principalTable: "BaseVehicle",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_BaseVehicle_BaseVehicleId",
                table: "Images",
                column: "BaseVehicleId",
                principalTable: "BaseVehicle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Features_BaseVehicle_BaseVehicleId",
                table: "Features");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_BaseVehicle_BaseVehicleId",
                table: "Images");

            migrationBuilder.DropTable(
                name: "BaseVehicle");

            migrationBuilder.DropIndex(
                name: "IX_Images_BaseVehicleId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Features_BaseVehicleId",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "BaseVehicleId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "BaseVehicleId",
                table: "Features");
        }
    }
}
