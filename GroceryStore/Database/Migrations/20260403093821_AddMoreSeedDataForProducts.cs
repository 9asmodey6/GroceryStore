using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GroceryStore.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreSeedDataForProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "attributes",
                columns: new[] { "id", "data_type", "max_value", "min_value", "name", "unit" },
                values: new object[,]
                {
                    { 17, 1, null, null, "Cut Type", null },
                    { 18, 1, null, null, "Meat Part", null },
                    { 19, 1, null, null, "Fish Type", null },
                    { 20, 1, null, null, "Processing Method", null },
                    { 21, 1, null, null, "Grain Type", null },
                    { 22, 2, 365m, 0m, "Shelf Life", "days" },
                    { 23, 3, 100m, 0m, "Protein Content", "%" },
                    { 24, 1, null, null, "Sweetness Level", null },
                    { 25, 1, null, null, "Variety", null },
                    { 26, 5, 1m, 0m, "Gluten Free", null },
                    { 27, 1, null, null, "Packaging Type", null },
                    { 28, 3, 500m, 0m, "Caffeine Content", "mg" },
                    { 29, 5, 1m, 0m, "Carbonation", null },
                    { 30, 1, null, null, "Chemical Type", null }
                });

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "id",
                keyValue: 7,
                column: "name",
                value: "Fruits & Vegetables");

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "name", "parent_id" },
                values: new object[,]
                {
                    { 34, "Beef", 3 },
                    { 35, "Pork", 3 },
                    { 36, "Poultry", 3 },
                    { 37, "Lamb", 3 },
                    { 38, "Ground Meat", 3 },
                    { 39, "Fresh Fish", 4 },
                    { 40, "Frozen Fish", 4 },
                    { 41, "Seafood", 4 },
                    { 42, "Canned Fish", 4 },
                    { 43, "Bread", 5 },
                    { 44, "Pastries", 5 },
                    { 45, "Cakes & Desserts", 5 },
                    { 46, "Vegetables", 7 },
                    { 47, "Fruits", 7 },
                    { 48, "Berries", 7 },
                    { 49, "Greens & Herbs", 7 },
                    { 50, "Soft Drinks", 8 },
                    { 51, "Juices", 8 },
                    { 52, "Water", 8 },
                    { 53, "Tea & Coffee", 8 },
                    { 54, "Cleaning Products", 10 },
                    { 55, "Laundry Detergents", 10 },
                    { 56, "Dishwashing", 10 },
                    { 57, "Shampoos & Conditioners", 11 },
                    { 58, "Body Care", 11 },
                    { 59, "Oral Care", 11 }
                });

            migrationBuilder.InsertData(
                table: "category_attributes",
                columns: new[] { "attribute_id", "category_id", "is_required" },
                values: new object[,]
                {
                    { 2, 5, true },
                    { 2, 7, true },
                    { 3, 8, true },
                    { 3, 10, true },
                    { 3, 11, true },
                    { 22, 1, true },
                    { 20, 3, true },
                    { 23, 3, true },
                    { 23, 4, true },
                    { 22, 5, true },
                    { 26, 5, false },
                    { 22, 8, true },
                    { 30, 10, true },
                    { 17, 34, false },
                    { 18, 34, true },
                    { 17, 35, false },
                    { 18, 35, true },
                    { 18, 36, true },
                    { 18, 37, true },
                    { 1, 38, true },
                    { 19, 39, true },
                    { 19, 40, true },
                    { 20, 41, true },
                    { 21, 43, true },
                    { 9, 44, true },
                    { 10, 44, false },
                    { 9, 45, true },
                    { 25, 46, false },
                    { 24, 47, false },
                    { 25, 47, false },
                    { 25, 48, false },
                    { 9, 50, true },
                    { 29, 50, true },
                    { 6, 51, false },
                    { 9, 51, true },
                    { 29, 52, true },
                    { 28, 53, false },
                    { 27, 54, false },
                    { 27, 55, false },
                    { 6, 57, false },
                    { 13, 58, false }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 22, 1 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 20, 3 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 23, 3 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 23, 4 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 2, 5 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 22, 5 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 26, 5 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 2, 7 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 3, 8 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 22, 8 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 3, 10 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 30, 10 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 3, 11 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 17, 34 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 18, 34 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 17, 35 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 18, 35 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 18, 36 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 18, 37 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 1, 38 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 19, 39 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 19, 40 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 20, 41 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 21, 43 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 9, 44 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 10, 44 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 9, 45 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 25, 46 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 24, 47 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 25, 47 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 25, 48 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 9, 50 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 29, 50 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 6, 51 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 9, 51 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 29, 52 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 28, 53 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 27, 54 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 27, 55 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 6, 57 });

            migrationBuilder.DeleteData(
                table: "category_attributes",
                keyColumns: new[] { "attribute_id", "category_id" },
                keyValues: new object[] { 13, 58 });

            migrationBuilder.DeleteData(
                table: "attributes",
                keyColumn: "id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "attributes",
                keyColumn: "id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "attributes",
                keyColumn: "id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "attributes",
                keyColumn: "id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "attributes",
                keyColumn: "id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "attributes",
                keyColumn: "id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "attributes",
                keyColumn: "id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "attributes",
                keyColumn: "id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "attributes",
                keyColumn: "id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "attributes",
                keyColumn: "id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "attributes",
                keyColumn: "id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "attributes",
                keyColumn: "id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "attributes",
                keyColumn: "id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "attributes",
                keyColumn: "id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 58);

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "id",
                keyValue: 7,
                column: "name",
                value: "Vegetables & Fruit");
        }
    }
}
