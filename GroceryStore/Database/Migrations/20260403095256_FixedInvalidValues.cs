using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GroceryStore.Migrations
{
    /// <inheritdoc />
    public partial class FixedInvalidValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 2, 12 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 2, 13 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 3, 14 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 3, 15 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 3, 16 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 3, 28 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 11, 28 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 11, 29 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 11, 30 });

            migrationBuilder.InsertData(
                table: "attributes",
                columns: new[] { "id", "data_type", "max_value", "min_value", "name", "unit" },
                values: new object[,]
                {
                    { 12, 3, 1000m, 0m, "Energy Value", "kcal/100g" },
                    { 14, 5, 1m, 0m, "Is Vegan", null },
                    { 15, 2, 100m, 1m, "Pieces per Pack", "pcs" }
                });

            migrationBuilder.InsertData(
                table: "category_attributes",
                columns: new[] { "attribute_id", "category_id", "is_required" },
                values: new object[,]
                {
                    { 3, 12, true },
                    { 3, 13, true },
                    { 2, 14, true },
                    { 2, 15, true },
                    { 2, 16, true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "attributes",
                keyColumn: "id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "attributes",
                keyColumn: "id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "attributes",
                keyColumn: "id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 3, 12 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 3, 13 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 2, 14 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 2, 15 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 2, 16 });

            migrationBuilder.InsertData(
                table: "category_attributes",
                columns: new[] { "attribute_id", "category_id", "is_required" },
                values: new object[,]
                {
                    { 2, 12, true },
                    { 2, 13, true },
                    { 3, 14, true },
                    { 3, 15, true },
                    { 3, 16, true },
                    { 3, 28, true },
                    { 11, 28, true },
                    { 11, 29, true },
                    { 11, 30, true }
                });
        }
    }
}
