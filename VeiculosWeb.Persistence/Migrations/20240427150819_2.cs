using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeiculosWeb.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverChecklistItems_DriverCategories_CategoryId",
                table: "DriverChecklistItems");

            migrationBuilder.DropForeignKey(
                name: "FK_DriverChecklistItems_DriverChecklistItems_ParentChecklistIt~",
                table: "DriverChecklistItems");

            migrationBuilder.DropForeignKey(
                name: "FK_DriverChecklistItems_DriverChecklists_ChecklistId",
                table: "DriverChecklistItems");

            migrationBuilder.DropForeignKey(
                name: "FK_DriverChecklistItems_DriverItems_ItemId",
                table: "DriverChecklistItems");

            migrationBuilder.DropForeignKey(
                name: "FK_DriverChecklistReplacedItems_DriverChecklistItems_Checklist~",
                table: "DriverChecklistReplacedItems");

            migrationBuilder.DropForeignKey(
                name: "FK_DriverChecklistReplacedItems_DriverChecklistReviews_Checkli~",
                table: "DriverChecklistReplacedItems");

            migrationBuilder.DropForeignKey(
                name: "FK_DriverChecklistReviews_DriverChecklists_ChecklistId",
                table: "DriverChecklistReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_DriverMedias_DriverChecklistReplacedItems_ChecklistCheckedI~",
                table: "DriverMedias");

            migrationBuilder.DropForeignKey(
                name: "FK_DriverOptions_DriverChecklistItems_ChecklistItemId",
                table: "DriverOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_ParamedicChecklistItems_ParamedicCategories_CategoryId",
                table: "ParamedicChecklistItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ParamedicChecklistItems_ParamedicChecklistItems_ParentCheck~",
                table: "ParamedicChecklistItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ParamedicChecklistItems_ParamedicChecklists_ChecklistId",
                table: "ParamedicChecklistItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ParamedicChecklistItems_ParamedicItems_ItemId",
                table: "ParamedicChecklistItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ParamedicChecklistReplacedItems_ParamedicChecklistItems_Che~",
                table: "ParamedicChecklistReplacedItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ParamedicChecklistReplacedItems_ParamedicChecklistReviews_C~",
                table: "ParamedicChecklistReplacedItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ParamedicChecklistReviews_ParamedicChecklists_ChecklistId",
                table: "ParamedicChecklistReviews");

            migrationBuilder.DropIndex(
                name: "IX_DriverChecklistReplacedItems_ChecklistReviewId",
                table: "DriverChecklistReplacedItems");

            migrationBuilder.DropIndex(
                name: "IX_DriverChecklistItems_ItemId_CategoryId_ChecklistId_TenantId~",
                table: "DriverChecklistItems");

            migrationBuilder.DropIndex(
                name: "IX_DriverChecklistItems_ParentChecklistItemId",
                table: "DriverChecklistItems");

            migrationBuilder.DropColumn(
                name: "ParentChecklistItemId",
                table: "DriverChecklistItems");

            migrationBuilder.RenameColumn(
                name: "ChecklistId",
                table: "ParamedicChecklistReviews",
                newName: "ParamedicChecklistId");

            migrationBuilder.RenameIndex(
                name: "IX_ParamedicChecklistReviews_ChecklistId",
                table: "ParamedicChecklistReviews",
                newName: "IX_ParamedicChecklistReviews_ParamedicChecklistId");

            migrationBuilder.RenameColumn(
                name: "ChecklistReviewId",
                table: "ParamedicChecklistReplacedItems",
                newName: "ParamedicChecklistReviewId");

            migrationBuilder.RenameColumn(
                name: "ChecklistItemId",
                table: "ParamedicChecklistReplacedItems",
                newName: "ParamedicChecklistItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ParamedicChecklistReplacedItems_ChecklistReviewId",
                table: "ParamedicChecklistReplacedItems",
                newName: "IX_ParamedicChecklistReplacedItems_ParamedicChecklistReviewId");

            migrationBuilder.RenameIndex(
                name: "IX_ParamedicChecklistReplacedItems_ChecklistItemId_ChecklistRe~",
                table: "ParamedicChecklistReplacedItems",
                newName: "IX_ParamedicChecklistReplacedItems_ParamedicChecklistItemId_Pa~");

            migrationBuilder.RenameColumn(
                name: "ParentChecklistItemId",
                table: "ParamedicChecklistItems",
                newName: "ParamedicParentChecklistItemId");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "ParamedicChecklistItems",
                newName: "ParamedicItemId");

            migrationBuilder.RenameColumn(
                name: "ChecklistId",
                table: "ParamedicChecklistItems",
                newName: "ParamedicChecklistId");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "ParamedicChecklistItems",
                newName: "ParamedicCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ParamedicChecklistItems_ParentChecklistItemId",
                table: "ParamedicChecklistItems",
                newName: "IX_ParamedicChecklistItems_ParamedicParentChecklistItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ParamedicChecklistItems_ItemId_CategoryId_ChecklistId_Tenan~",
                table: "ParamedicChecklistItems",
                newName: "IX_ParamedicChecklistItems_ParamedicItemId_ParamedicCategoryId~");

            migrationBuilder.RenameIndex(
                name: "IX_ParamedicChecklistItems_ChecklistId",
                table: "ParamedicChecklistItems",
                newName: "IX_ParamedicChecklistItems_ParamedicChecklistId");

            migrationBuilder.RenameIndex(
                name: "IX_ParamedicChecklistItems_CategoryId",
                table: "ParamedicChecklistItems",
                newName: "IX_ParamedicChecklistItems_ParamedicCategoryId");

            migrationBuilder.RenameColumn(
                name: "ChecklistItemId",
                table: "DriverOptions",
                newName: "DriverChecklistItemId");

            migrationBuilder.RenameIndex(
                name: "IX_DriverOptions_ChecklistItemId",
                table: "DriverOptions",
                newName: "IX_DriverOptions_DriverChecklistItemId");

            migrationBuilder.RenameColumn(
                name: "ChecklistCheckedItemId",
                table: "DriverMedias",
                newName: "DriverChecklistCheckedItemId");

            migrationBuilder.RenameIndex(
                name: "IX_DriverMedias_ChecklistCheckedItemId",
                table: "DriverMedias",
                newName: "IX_DriverMedias_DriverChecklistCheckedItemId");

            migrationBuilder.RenameColumn(
                name: "ChecklistId",
                table: "DriverChecklistReviews",
                newName: "DriverChecklistId");

            migrationBuilder.RenameIndex(
                name: "IX_DriverChecklistReviews_ChecklistId",
                table: "DriverChecklistReviews",
                newName: "IX_DriverChecklistReviews_DriverChecklistId");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "DriverChecklistItems",
                newName: "DriverItemId");

            migrationBuilder.RenameColumn(
                name: "ChecklistId",
                table: "DriverChecklistItems",
                newName: "DriverChecklistId");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "DriverChecklistItems",
                newName: "DriverCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_DriverChecklistItems_ChecklistId",
                table: "DriverChecklistItems",
                newName: "IX_DriverChecklistItems_DriverChecklistId");

            migrationBuilder.RenameIndex(
                name: "IX_DriverChecklistItems_CategoryId",
                table: "DriverChecklistItems",
                newName: "IX_DriverChecklistItems_DriverCategoryId");

            migrationBuilder.AlterColumn<Guid>(
                name: "TenantId",
                table: "ParamedicItems",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "TenantId",
                table: "ParamedicChecklists",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "TenantId",
                table: "ParamedicChecklistReviews",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "TenantId",
                table: "ParamedicChecklistReplacedItems",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "TenantId",
                table: "ParamedicChecklistItems",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "TenantId",
                table: "ParamedicCategories",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "TenantId",
                table: "DriverOptions",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "TenantId",
                table: "DriverMedias",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "TenantId",
                table: "DriverItems",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "TenantId",
                table: "DriverChecklists",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "TenantId",
                table: "DriverChecklistReviews",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "TenantId",
                table: "DriverChecklistReplacedItems",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "DriverChecklistItemId",
                table: "DriverChecklistReplacedItems",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DriverChecklistReviewId",
                table: "DriverChecklistReplacedItems",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "TenantId",
                table: "DriverChecklistItems",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "TenantId",
                table: "DriverCategories",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "TenantId",
                table: "Ambulances",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateIndex(
                name: "IX_DriverChecklistReplacedItems_DriverChecklistItemId",
                table: "DriverChecklistReplacedItems",
                column: "DriverChecklistItemId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverChecklistReplacedItems_DriverChecklistReviewId",
                table: "DriverChecklistReplacedItems",
                column: "DriverChecklistReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverChecklistItems_DriverItemId_DriverCategoryId_DriverCh~",
                table: "DriverChecklistItems",
                columns: new[] { "DriverItemId", "DriverCategoryId", "DriverChecklistId", "TenantId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DriverChecklistItems_DriverCategories_DriverCategoryId",
                table: "DriverChecklistItems",
                column: "DriverCategoryId",
                principalTable: "DriverCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DriverChecklistItems_DriverChecklists_DriverChecklistId",
                table: "DriverChecklistItems",
                column: "DriverChecklistId",
                principalTable: "DriverChecklists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DriverChecklistItems_DriverItems_DriverItemId",
                table: "DriverChecklistItems",
                column: "DriverItemId",
                principalTable: "DriverItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_DriverChecklistReviews_DriverChecklists_DriverChecklistId",
                table: "DriverChecklistReviews",
                column: "DriverChecklistId",
                principalTable: "DriverChecklists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DriverMedias_DriverChecklistReplacedItems_DriverChecklistCh~",
                table: "DriverMedias",
                column: "DriverChecklistCheckedItemId",
                principalTable: "DriverChecklistReplacedItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DriverOptions_DriverChecklistItems_DriverChecklistItemId",
                table: "DriverOptions",
                column: "DriverChecklistItemId",
                principalTable: "DriverChecklistItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParamedicChecklistItems_ParamedicCategories_ParamedicCatego~",
                table: "ParamedicChecklistItems",
                column: "ParamedicCategoryId",
                principalTable: "ParamedicCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParamedicChecklistItems_ParamedicChecklistItems_ParamedicPa~",
                table: "ParamedicChecklistItems",
                column: "ParamedicParentChecklistItemId",
                principalTable: "ParamedicChecklistItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ParamedicChecklistItems_ParamedicChecklists_ParamedicCheckl~",
                table: "ParamedicChecklistItems",
                column: "ParamedicChecklistId",
                principalTable: "ParamedicChecklists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParamedicChecklistItems_ParamedicItems_ParamedicItemId",
                table: "ParamedicChecklistItems",
                column: "ParamedicItemId",
                principalTable: "ParamedicItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParamedicChecklistReplacedItems_ParamedicChecklistItems_Par~",
                table: "ParamedicChecklistReplacedItems",
                column: "ParamedicChecklistItemId",
                principalTable: "ParamedicChecklistItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParamedicChecklistReplacedItems_ParamedicChecklistReviews_P~",
                table: "ParamedicChecklistReplacedItems",
                column: "ParamedicChecklistReviewId",
                principalTable: "ParamedicChecklistReviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParamedicChecklistReviews_ParamedicChecklists_ParamedicChec~",
                table: "ParamedicChecklistReviews",
                column: "ParamedicChecklistId",
                principalTable: "ParamedicChecklists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverChecklistItems_DriverCategories_DriverCategoryId",
                table: "DriverChecklistItems");

            migrationBuilder.DropForeignKey(
                name: "FK_DriverChecklistItems_DriverChecklists_DriverChecklistId",
                table: "DriverChecklistItems");

            migrationBuilder.DropForeignKey(
                name: "FK_DriverChecklistItems_DriverItems_DriverItemId",
                table: "DriverChecklistItems");

            migrationBuilder.DropForeignKey(
                name: "FK_DriverChecklistReplacedItems_DriverChecklistItems_DriverChe~",
                table: "DriverChecklistReplacedItems");

            migrationBuilder.DropForeignKey(
                name: "FK_DriverChecklistReplacedItems_DriverChecklistReviews_DriverC~",
                table: "DriverChecklistReplacedItems");

            migrationBuilder.DropForeignKey(
                name: "FK_DriverChecklistReviews_DriverChecklists_DriverChecklistId",
                table: "DriverChecklistReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_DriverMedias_DriverChecklistReplacedItems_DriverChecklistCh~",
                table: "DriverMedias");

            migrationBuilder.DropForeignKey(
                name: "FK_DriverOptions_DriverChecklistItems_DriverChecklistItemId",
                table: "DriverOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_ParamedicChecklistItems_ParamedicCategories_ParamedicCatego~",
                table: "ParamedicChecklistItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ParamedicChecklistItems_ParamedicChecklistItems_ParamedicPa~",
                table: "ParamedicChecklistItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ParamedicChecklistItems_ParamedicChecklists_ParamedicCheckl~",
                table: "ParamedicChecklistItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ParamedicChecklistItems_ParamedicItems_ParamedicItemId",
                table: "ParamedicChecklistItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ParamedicChecklistReplacedItems_ParamedicChecklistItems_Par~",
                table: "ParamedicChecklistReplacedItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ParamedicChecklistReplacedItems_ParamedicChecklistReviews_P~",
                table: "ParamedicChecklistReplacedItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ParamedicChecklistReviews_ParamedicChecklists_ParamedicChec~",
                table: "ParamedicChecklistReviews");

            migrationBuilder.DropIndex(
                name: "IX_DriverChecklistReplacedItems_DriverChecklistItemId",
                table: "DriverChecklistReplacedItems");

            migrationBuilder.DropIndex(
                name: "IX_DriverChecklistReplacedItems_DriverChecklistReviewId",
                table: "DriverChecklistReplacedItems");

            migrationBuilder.DropIndex(
                name: "IX_DriverChecklistItems_DriverItemId_DriverCategoryId_DriverCh~",
                table: "DriverChecklistItems");

            migrationBuilder.DropColumn(
                name: "DriverChecklistItemId",
                table: "DriverChecklistReplacedItems");

            migrationBuilder.DropColumn(
                name: "DriverChecklistReviewId",
                table: "DriverChecklistReplacedItems");

            migrationBuilder.RenameColumn(
                name: "ParamedicChecklistId",
                table: "ParamedicChecklistReviews",
                newName: "ChecklistId");

            migrationBuilder.RenameIndex(
                name: "IX_ParamedicChecklistReviews_ParamedicChecklistId",
                table: "ParamedicChecklistReviews",
                newName: "IX_ParamedicChecklistReviews_ChecklistId");

            migrationBuilder.RenameColumn(
                name: "ParamedicChecklistReviewId",
                table: "ParamedicChecklistReplacedItems",
                newName: "ChecklistReviewId");

            migrationBuilder.RenameColumn(
                name: "ParamedicChecklistItemId",
                table: "ParamedicChecklistReplacedItems",
                newName: "ChecklistItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ParamedicChecklistReplacedItems_ParamedicChecklistReviewId",
                table: "ParamedicChecklistReplacedItems",
                newName: "IX_ParamedicChecklistReplacedItems_ChecklistReviewId");

            migrationBuilder.RenameIndex(
                name: "IX_ParamedicChecklistReplacedItems_ParamedicChecklistItemId_Pa~",
                table: "ParamedicChecklistReplacedItems",
                newName: "IX_ParamedicChecklistReplacedItems_ChecklistItemId_ChecklistRe~");

            migrationBuilder.RenameColumn(
                name: "ParamedicParentChecklistItemId",
                table: "ParamedicChecklistItems",
                newName: "ParentChecklistItemId");

            migrationBuilder.RenameColumn(
                name: "ParamedicItemId",
                table: "ParamedicChecklistItems",
                newName: "ItemId");

            migrationBuilder.RenameColumn(
                name: "ParamedicChecklistId",
                table: "ParamedicChecklistItems",
                newName: "ChecklistId");

            migrationBuilder.RenameColumn(
                name: "ParamedicCategoryId",
                table: "ParamedicChecklistItems",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ParamedicChecklistItems_ParamedicParentChecklistItemId",
                table: "ParamedicChecklistItems",
                newName: "IX_ParamedicChecklistItems_ParentChecklistItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ParamedicChecklistItems_ParamedicItemId_ParamedicCategoryId~",
                table: "ParamedicChecklistItems",
                newName: "IX_ParamedicChecklistItems_ItemId_CategoryId_ChecklistId_Tenan~");

            migrationBuilder.RenameIndex(
                name: "IX_ParamedicChecklistItems_ParamedicChecklistId",
                table: "ParamedicChecklistItems",
                newName: "IX_ParamedicChecklistItems_ChecklistId");

            migrationBuilder.RenameIndex(
                name: "IX_ParamedicChecklistItems_ParamedicCategoryId",
                table: "ParamedicChecklistItems",
                newName: "IX_ParamedicChecklistItems_CategoryId");

            migrationBuilder.RenameColumn(
                name: "DriverChecklistItemId",
                table: "DriverOptions",
                newName: "ChecklistItemId");

            migrationBuilder.RenameIndex(
                name: "IX_DriverOptions_DriverChecklistItemId",
                table: "DriverOptions",
                newName: "IX_DriverOptions_ChecklistItemId");

            migrationBuilder.RenameColumn(
                name: "DriverChecklistCheckedItemId",
                table: "DriverMedias",
                newName: "ChecklistCheckedItemId");

            migrationBuilder.RenameIndex(
                name: "IX_DriverMedias_DriverChecklistCheckedItemId",
                table: "DriverMedias",
                newName: "IX_DriverMedias_ChecklistCheckedItemId");

            migrationBuilder.RenameColumn(
                name: "DriverChecklistId",
                table: "DriverChecklistReviews",
                newName: "ChecklistId");

            migrationBuilder.RenameIndex(
                name: "IX_DriverChecklistReviews_DriverChecklistId",
                table: "DriverChecklistReviews",
                newName: "IX_DriverChecklistReviews_ChecklistId");

            migrationBuilder.RenameColumn(
                name: "DriverItemId",
                table: "DriverChecklistItems",
                newName: "ItemId");

            migrationBuilder.RenameColumn(
                name: "DriverChecklistId",
                table: "DriverChecklistItems",
                newName: "ChecklistId");

            migrationBuilder.RenameColumn(
                name: "DriverCategoryId",
                table: "DriverChecklistItems",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_DriverChecklistItems_DriverChecklistId",
                table: "DriverChecklistItems",
                newName: "IX_DriverChecklistItems_ChecklistId");

            migrationBuilder.RenameIndex(
                name: "IX_DriverChecklistItems_DriverCategoryId",
                table: "DriverChecklistItems",
                newName: "IX_DriverChecklistItems_CategoryId");

            migrationBuilder.AlterColumn<Guid>(
                name: "TenantId",
                table: "ParamedicItems",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TenantId",
                table: "ParamedicChecklists",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TenantId",
                table: "ParamedicChecklistReviews",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TenantId",
                table: "ParamedicChecklistReplacedItems",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TenantId",
                table: "ParamedicChecklistItems",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TenantId",
                table: "ParamedicCategories",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TenantId",
                table: "DriverOptions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TenantId",
                table: "DriverMedias",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TenantId",
                table: "DriverItems",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TenantId",
                table: "DriverChecklists",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TenantId",
                table: "DriverChecklistReviews",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TenantId",
                table: "DriverChecklistReplacedItems",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TenantId",
                table: "DriverChecklistItems",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ParentChecklistItemId",
                table: "DriverChecklistItems",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TenantId",
                table: "DriverCategories",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TenantId",
                table: "Ambulances",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DriverChecklistReplacedItems_ChecklistReviewId",
                table: "DriverChecklistReplacedItems",
                column: "ChecklistReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverChecklistItems_ItemId_CategoryId_ChecklistId_TenantId~",
                table: "DriverChecklistItems",
                columns: new[] { "ItemId", "CategoryId", "ChecklistId", "TenantId", "ParentChecklistItemId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DriverChecklistItems_ParentChecklistItemId",
                table: "DriverChecklistItems",
                column: "ParentChecklistItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_DriverChecklistItems_DriverCategories_CategoryId",
                table: "DriverChecklistItems",
                column: "CategoryId",
                principalTable: "DriverCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DriverChecklistItems_DriverChecklistItems_ParentChecklistIt~",
                table: "DriverChecklistItems",
                column: "ParentChecklistItemId",
                principalTable: "DriverChecklistItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DriverChecklistItems_DriverChecklists_ChecklistId",
                table: "DriverChecklistItems",
                column: "ChecklistId",
                principalTable: "DriverChecklists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DriverChecklistItems_DriverItems_ItemId",
                table: "DriverChecklistItems",
                column: "ItemId",
                principalTable: "DriverItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DriverChecklistReplacedItems_DriverChecklistItems_Checklist~",
                table: "DriverChecklistReplacedItems",
                column: "ChecklistItemId",
                principalTable: "DriverChecklistItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DriverChecklistReplacedItems_DriverChecklistReviews_Checkli~",
                table: "DriverChecklistReplacedItems",
                column: "ChecklistReviewId",
                principalTable: "DriverChecklistReviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DriverChecklistReviews_DriverChecklists_ChecklistId",
                table: "DriverChecklistReviews",
                column: "ChecklistId",
                principalTable: "DriverChecklists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DriverMedias_DriverChecklistReplacedItems_ChecklistCheckedI~",
                table: "DriverMedias",
                column: "ChecklistCheckedItemId",
                principalTable: "DriverChecklistReplacedItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DriverOptions_DriverChecklistItems_ChecklistItemId",
                table: "DriverOptions",
                column: "ChecklistItemId",
                principalTable: "DriverChecklistItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParamedicChecklistItems_ParamedicCategories_CategoryId",
                table: "ParamedicChecklistItems",
                column: "CategoryId",
                principalTable: "ParamedicCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParamedicChecklistItems_ParamedicChecklistItems_ParentCheck~",
                table: "ParamedicChecklistItems",
                column: "ParentChecklistItemId",
                principalTable: "ParamedicChecklistItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ParamedicChecklistItems_ParamedicChecklists_ChecklistId",
                table: "ParamedicChecklistItems",
                column: "ChecklistId",
                principalTable: "ParamedicChecklists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParamedicChecklistItems_ParamedicItems_ItemId",
                table: "ParamedicChecklistItems",
                column: "ItemId",
                principalTable: "ParamedicItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParamedicChecklistReplacedItems_ParamedicChecklistItems_Che~",
                table: "ParamedicChecklistReplacedItems",
                column: "ChecklistItemId",
                principalTable: "ParamedicChecklistItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParamedicChecklistReplacedItems_ParamedicChecklistReviews_C~",
                table: "ParamedicChecklistReplacedItems",
                column: "ChecklistReviewId",
                principalTable: "ParamedicChecklistReviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParamedicChecklistReviews_ParamedicChecklists_ChecklistId",
                table: "ParamedicChecklistReviews",
                column: "ChecklistId",
                principalTable: "ParamedicChecklists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
