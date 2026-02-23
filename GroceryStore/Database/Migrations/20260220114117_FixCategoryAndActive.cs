using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroceryStore.Migrations
{
    /// <inheritdoc />
    public partial class FixCategoryAndActive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "f_k_categories_categories_parent_id",
                table: "categories");

            migrationBuilder.RenameColumn(
                name: "is_asctive",
                table: "products",
                newName: "is_active");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "expiration_date",
                table: "product_batches",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<int>(
                name: "data_type",
                table: "attributes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "f_k_categories_categories_parent_id",
                table: "categories",
                column: "parent_id",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "f_k_categories_categories_parent_id",
                table: "categories");

            migrationBuilder.RenameColumn(
                name: "is_active",
                table: "products",
                newName: "is_asctive");

            migrationBuilder.AlterColumn<DateTime>(
                name: "expiration_date",
                table: "product_batches",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "data_type",
                table: "attributes",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "f_k_categories_categories_parent_id",
                table: "categories",
                column: "parent_id",
                principalTable: "categories",
                principalColumn: "id");
        }
    }
}
