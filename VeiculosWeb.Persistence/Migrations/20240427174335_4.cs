using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeiculosWeb.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverChecklistReplacedItems_DriverChecklistItems_DriverChe~",
                table: "DriverChecklistReplacedItems");

            migrationBuilder.DropForeignKey(
                name: "FK_DriverChecklistReplacedItems_DriverChecklistReviews_DriverC~",
                table: "DriverChecklistReplacedItems");

            migrationBuilder.DropForeignKey(
                name: "FK_DriverChecklistReplacedItems_DriverOptions_OptionId",
                table: "DriverChecklistReplacedItems");

            migrationBuilder.DropForeignKey(
                name: "FK_DriverMedias_DriverChecklistReplacedItems_DriverChecklistCh~",
                table: "DriverMedias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DriverChecklistReplacedItems",
                table: "DriverChecklistReplacedItems");

            migrationBuilder.RenameTable(
                name: "DriverChecklistReplacedItems",
                newName: "DriverChecklistCheckedItems");

            migrationBuilder.RenameIndex(
                name: "IX_DriverChecklistReplacedItems_OptionId",
                table: "DriverChecklistCheckedItems",
                newName: "IX_DriverChecklistCheckedItems_OptionId");

            migrationBuilder.RenameIndex(
                name: "IX_DriverChecklistReplacedItems_DriverChecklistReviewId",
                table: "DriverChecklistCheckedItems",
                newName: "IX_DriverChecklistCheckedItems_DriverChecklistReviewId");

            migrationBuilder.RenameIndex(
                name: "IX_DriverChecklistReplacedItems_DriverChecklistItemId",
                table: "DriverChecklistCheckedItems",
                newName: "IX_DriverChecklistCheckedItems_DriverChecklistItemId");

            migrationBuilder.RenameIndex(
                name: "IX_DriverChecklistReplacedItems_ChecklistItemId_ChecklistRevie~",
                table: "DriverChecklistCheckedItems",
                newName: "IX_DriverChecklistCheckedItems_ChecklistItemId_ChecklistReview~");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DriverChecklistCheckedItems",
                table: "DriverChecklistCheckedItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DriverChecklistCheckedItems_DriverChecklistItems_DriverChec~",
                table: "DriverChecklistCheckedItems",
                column: "DriverChecklistItemId",
                principalTable: "DriverChecklistItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DriverChecklistCheckedItems_DriverChecklistReviews_DriverCh~",
                table: "DriverChecklistCheckedItems",
                column: "DriverChecklistReviewId",
                principalTable: "DriverChecklistReviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DriverChecklistCheckedItems_DriverOptions_OptionId",
                table: "DriverChecklistCheckedItems",
                column: "OptionId",
                principalTable: "DriverOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DriverMedias_DriverChecklistCheckedItems_DriverChecklistChe~",
                table: "DriverMedias",
                column: "DriverChecklistCheckedItemId",
                principalTable: "DriverChecklistCheckedItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverChecklistCheckedItems_DriverChecklistItems_DriverChec~",
                table: "DriverChecklistCheckedItems");

            migrationBuilder.DropForeignKey(
                name: "FK_DriverChecklistCheckedItems_DriverChecklistReviews_DriverCh~",
                table: "DriverChecklistCheckedItems");

            migrationBuilder.DropForeignKey(
                name: "FK_DriverChecklistCheckedItems_DriverOptions_OptionId",
                table: "DriverChecklistCheckedItems");

            migrationBuilder.DropForeignKey(
                name: "FK_DriverMedias_DriverChecklistCheckedItems_DriverChecklistChe~",
                table: "DriverMedias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DriverChecklistCheckedItems",
                table: "DriverChecklistCheckedItems");

            migrationBuilder.RenameTable(
                name: "DriverChecklistCheckedItems",
                newName: "DriverChecklistReplacedItems");

            migrationBuilder.RenameIndex(
                name: "IX_DriverChecklistCheckedItems_OptionId",
                table: "DriverChecklistReplacedItems",
                newName: "IX_DriverChecklistReplacedItems_OptionId");

            migrationBuilder.RenameIndex(
                name: "IX_DriverChecklistCheckedItems_DriverChecklistReviewId",
                table: "DriverChecklistReplacedItems",
                newName: "IX_DriverChecklistReplacedItems_DriverChecklistReviewId");

            migrationBuilder.RenameIndex(
                name: "IX_DriverChecklistCheckedItems_DriverChecklistItemId",
                table: "DriverChecklistReplacedItems",
                newName: "IX_DriverChecklistReplacedItems_DriverChecklistItemId");

            migrationBuilder.RenameIndex(
                name: "IX_DriverChecklistCheckedItems_ChecklistItemId_ChecklistReview~",
                table: "DriverChecklistReplacedItems",
                newName: "IX_DriverChecklistReplacedItems_ChecklistItemId_ChecklistRevie~");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DriverChecklistReplacedItems",
                table: "DriverChecklistReplacedItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DriverChecklistReplacedItems_DriverChecklistItems_DriverChe~",
                table: "DriverChecklistReplacedItems",
                column: "DriverChecklistItemId",
                principalTable: "DriverChecklistItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DriverChecklistReplacedItems_DriverChecklistReviews_DriverC~",
                table: "DriverChecklistReplacedItems",
                column: "DriverChecklistReviewId",
                principalTable: "DriverChecklistReviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DriverChecklistReplacedItems_DriverOptions_OptionId",
                table: "DriverChecklistReplacedItems",
                column: "OptionId",
                principalTable: "DriverOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DriverMedias_DriverChecklistReplacedItems_DriverChecklistCh~",
                table: "DriverMedias",
                column: "DriverChecklistCheckedItemId",
                principalTable: "DriverChecklistReplacedItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
