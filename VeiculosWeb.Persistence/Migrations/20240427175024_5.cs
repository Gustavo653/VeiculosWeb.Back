using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeiculosWeb.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverChecklistCheckedItems_DriverOptions_OptionId",
                table: "DriverChecklistCheckedItems");

            migrationBuilder.DropIndex(
                name: "IX_DriverChecklistCheckedItems_ChecklistItemId_ChecklistReview~",
                table: "DriverChecklistCheckedItems");

            migrationBuilder.DropIndex(
                name: "IX_DriverChecklistCheckedItems_DriverChecklistItemId",
                table: "DriverChecklistCheckedItems");

            migrationBuilder.DropColumn(
                name: "ChecklistItemId",
                table: "DriverChecklistCheckedItems");

            migrationBuilder.DropColumn(
                name: "ChecklistReviewId",
                table: "DriverChecklistCheckedItems");

            migrationBuilder.RenameColumn(
                name: "OptionId",
                table: "DriverChecklistCheckedItems",
                newName: "DriverOptionId");

            migrationBuilder.RenameIndex(
                name: "IX_DriverChecklistCheckedItems_OptionId",
                table: "DriverChecklistCheckedItems",
                newName: "IX_DriverChecklistCheckedItems_DriverOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverChecklistCheckedItems_DriverChecklistItemId_DriverChe~",
                table: "DriverChecklistCheckedItems",
                columns: new[] { "DriverChecklistItemId", "DriverChecklistReviewId", "TenantId", "DriverOptionId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DriverChecklistCheckedItems_DriverOptions_DriverOptionId",
                table: "DriverChecklistCheckedItems",
                column: "DriverOptionId",
                principalTable: "DriverOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverChecklistCheckedItems_DriverOptions_DriverOptionId",
                table: "DriverChecklistCheckedItems");

            migrationBuilder.DropIndex(
                name: "IX_DriverChecklistCheckedItems_DriverChecklistItemId_DriverChe~",
                table: "DriverChecklistCheckedItems");

            migrationBuilder.RenameColumn(
                name: "DriverOptionId",
                table: "DriverChecklistCheckedItems",
                newName: "OptionId");

            migrationBuilder.RenameIndex(
                name: "IX_DriverChecklistCheckedItems_DriverOptionId",
                table: "DriverChecklistCheckedItems",
                newName: "IX_DriverChecklistCheckedItems_OptionId");

            migrationBuilder.AddColumn<Guid>(
                name: "ChecklistItemId",
                table: "DriverChecklistCheckedItems",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ChecklistReviewId",
                table: "DriverChecklistCheckedItems",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_DriverChecklistCheckedItems_ChecklistItemId_ChecklistReview~",
                table: "DriverChecklistCheckedItems",
                columns: new[] { "ChecklistItemId", "ChecklistReviewId", "TenantId", "OptionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DriverChecklistCheckedItems_DriverChecklistItemId",
                table: "DriverChecklistCheckedItems",
                column: "DriverChecklistItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_DriverChecklistCheckedItems_DriverOptions_OptionId",
                table: "DriverChecklistCheckedItems",
                column: "OptionId",
                principalTable: "DriverOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
