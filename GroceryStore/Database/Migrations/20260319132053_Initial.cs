using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GroceryStore.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "asp_net_roles",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_asp_net_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "asp_net_users",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    first_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: true),
                    security_stamp = table.Column<string>(type: "text", nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    phone_number_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    two_factor_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    lockout_end = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    lockout_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    access_failed_count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_asp_net_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "attributes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    data_type = table.Column<int>(type: "integer", nullable: false),
                    unit = table.Column<string>(type: "text", nullable: true),
                    min_value = table.Column<decimal>(type: "numeric", nullable: true),
                    max_value = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_attributes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "brands",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_brands", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    parent_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_categories", x => x.id);
                    table.ForeignKey(
                        name: "f_k_categories_categories_parent_id",
                        column: x => x.parent_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "countries",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    code = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_countries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "asp_net_role_claims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_id = table.Column<string>(type: "text", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_asp_net_role_claims", x => x.id);
                    table.ForeignKey(
                        name: "f_k_asp_net_role_claims_asp_net_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "asp_net_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "asp_net_user_claims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_asp_net_user_claims", x => x.id);
                    table.ForeignKey(
                        name: "f_k_asp_net_user_claims_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "asp_net_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "asp_net_user_logins",
                columns: table => new
                {
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    provider_key = table.Column<string>(type: "text", nullable: false),
                    provider_display_name = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_asp_net_user_logins", x => new { x.login_provider, x.provider_key });
                    table.ForeignKey(
                        name: "f_k_asp_net_user_logins_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "asp_net_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "asp_net_user_roles",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "text", nullable: false),
                    role_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_asp_net_user_roles", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "f_k_asp_net_user_roles_asp_net_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "asp_net_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "f_k_asp_net_user_roles_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "asp_net_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "asp_net_user_tokens",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "text", nullable: false),
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_asp_net_user_tokens", x => new { x.user_id, x.login_provider, x.name });
                    table.ForeignKey(
                        name: "f_k_asp_net_user_tokens_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "asp_net_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "category_attributes",
                columns: table => new
                {
                    category_id = table.Column<int>(type: "integer", nullable: false),
                    attribute_id = table.Column<int>(type: "integer", nullable: false),
                    is_required = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_category_attributes", x => new { x.category_id, x.attribute_id });
                    table.ForeignKey(
                        name: "f_k_category_attributes_attributes_attribute_id",
                        column: x => x.attribute_id,
                        principalTable: "attributes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "f_k_category_attributes_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    category_id = table.Column<int>(type: "integer", nullable: false),
                    brand_id = table.Column<int>(type: "integer", nullable: false),
                    country_id = table.Column<int>(type: "integer", nullable: false),
                    sku = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    base_unit = table.Column<string>(type: "text", nullable: true, defaultValue: "pcs"),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    metadata = table.Column<string>(type: "jsonb", nullable: false, defaultValueSql: "'{}'::jsonb")
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_products", x => x.id);
                    table.ForeignKey(
                        name: "f_k_products_brands_brand_id",
                        column: x => x.brand_id,
                        principalTable: "brands",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "f_k_products_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "f_k_products_countries_country_id",
                        column: x => x.country_id,
                        principalTable: "countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "product_batches",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    quantity_arrived = table.Column<int>(type: "integer", nullable: false),
                    quantity_remaining = table.Column<int>(type: "integer", nullable: false),
                    batch_number = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    product_id = table.Column<int>(type: "integer", nullable: false),
                    purchase_price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    supply_date = table.Column<DateOnly>(type: "date", nullable: false),
                    expiration_date = table.Column<DateOnly>(type: "date", nullable: false),
                    is_closed = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_product_batches", x => x.id);
                    table.ForeignKey(
                        name: "f_k_product_batches_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "asp_net_roles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[,]
                {
                    { "2c5e174e-3b0e-446f-86af-483d56fd7210", "b7a5a87b-4024-46c5-8422-92182065842c", "Admin", "ADMIN" },
                    { "3d5e174e-3b0e-446f-86af-483d56fd7211", "c8b6b98c-5135-47d6-9533-03293176953d", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "asp_net_users",
                columns: new[] { "id", "access_failed_count", "concurrency_stamp", "created_at", "email", "email_confirmed", "first_name", "last_name", "lockout_enabled", "lockout_end", "normalized_email", "normalized_user_name", "password_hash", "phone_number", "phone_number_confirmed", "security_stamp", "two_factor_enabled", "user_name" },
                values: new object[] { "8e445865-a24d-4543-a6c6-9443d048cdb9", 0, "b7a5a87b-4024-46c5-8422-92182065842c", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "admin@store.com", true, "Admin", "System", true, null, "ADMIN@STORE.COM", "ADMIN@STORE.COM", "AQAAAAIAAYagAAAAEGMNZBXRiyiRbWqWDC6BZpBazgpNB5dAYizy/o2VwSgudvnw/5sqpGVlsFAp9P57WA==", null, false, "9ca06830-67d7-4632-9c32-f288924b893f", false, "admin@store.com" });

            migrationBuilder.InsertData(
                table: "attributes",
                columns: new[] { "id", "data_type", "max_value", "min_value", "name", "unit" },
                values: new object[,]
                {
                    { 1, 3, 100m, 0m, "Fat Content", "%" },
                    { 2, 2, 10000m, 0m, "Weight", "g" },
                    { 3, 2, 10000m, 0m, "Volume", "ml" },
                    { 4, 1, null, null, "Milk Source", null },
                    { 5, 1, null, null, "Base Ingredient", null },
                    { 6, 1, null, null, "Flavor", null },
                    { 7, 1, null, null, "Mold Type", null },
                    { 8, 5, 1m, 0m, "Lactose Free", null },
                    { 9, 3, 100m, 0m, "Sugar Content", "%" },
                    { 10, 5, 1m, 0m, "Contains Filling", null },
                    { 11, 3, 100m, 0m, "Alcohol Content", "%" },
                    { 13, 5, 1m, 0m, "Organic", null },
                    { 16, 3, null, null, "Temperature Storage", null }
                });

            migrationBuilder.InsertData(
                table: "brands",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Generic" },
                    { 2, "Coca-Cola" },
                    { 3, "Pepsi" }
                });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "name", "parent_id" },
                values: new object[,]
                {
                    { 1, "Dairy Products", null },
                    { 2, "Eggs", null },
                    { 3, "Meat & Poultry", null },
                    { 4, "Fish & Seafood", null },
                    { 5, "Bakery & Pastry", null },
                    { 6, "Grocery", null },
                    { 7, "Vegetables & Fruit", null },
                    { 8, "Beverages", null },
                    { 9, "Alcohol", null },
                    { 10, "Household Chemicals", null },
                    { 11, "Personal Care & Hygiene", null }
                });

            migrationBuilder.InsertData(
                table: "countries",
                columns: new[] { "id", "code", "name" },
                values: new object[,]
                {
                    { 1, "UA", "Ukraine" },
                    { 2, "PL", "Poland" },
                    { 3, "DE", "Germany" }
                });

            migrationBuilder.InsertData(
                table: "asp_net_user_roles",
                columns: new[] { "role_id", "user_id" },
                values: new object[] { "2c5e174e-3b0e-446f-86af-483d56fd7210", "8e445865-a24d-4543-a6c6-9443d048cdb9" });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "name", "parent_id" },
                values: new object[,]
                {
                    { 12, "Milk", 1 },
                    { 13, "Drinkable Fermented", 1 },
                    { 14, "Spoonable Dairy & Sour Cream", 1 },
                    { 15, "Cheese & Cottage Cheese", 1 },
                    { 16, "Butter", 1 },
                    { 24, "Low Alcohol", 9 },
                    { 25, "Strong Alcohol", 9 }
                });

            migrationBuilder.InsertData(
                table: "category_attributes",
                columns: new[] { "attribute_id", "category_id", "is_required" },
                values: new object[,]
                {
                    { 1, 1, true },
                    { 8, 1, true },
                    { 2, 3, true },
                    { 16, 3, true },
                    { 2, 4, true },
                    { 16, 4, true },
                    { 2, 6, true },
                    { 13, 7, false },
                    { 3, 9, true },
                    { 11, 9, true }
                });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "name", "parent_id" },
                values: new object[,]
                {
                    { 17, "Animal Milk", 12 },
                    { 18, "Plant-Based Milk", 12 },
                    { 19, "Traditional Kefir", 13 },
                    { 20, "Drinkable Yogurt", 13 },
                    { 21, "Hard Cheese", 15 },
                    { 22, "Soft & Brine Cheese", 15 },
                    { 23, "Mold Cheese", 15 },
                    { 26, "Beer", 24 },
                    { 27, "Cider", 24 },
                    { 28, "Vodka", 25 },
                    { 29, "Whiskey", 25 },
                    { 30, "Cognac", 25 },
                    { 31, "Rum", 25 },
                    { 32, "Tequila", 25 },
                    { 33, "Gin", 25 }
                });

            migrationBuilder.InsertData(
                table: "category_attributes",
                columns: new[] { "attribute_id", "category_id", "is_required" },
                values: new object[,]
                {
                    { 2, 12, true },
                    { 4, 12, true },
                    { 2, 13, true },
                    { 3, 14, true },
                    { 6, 14, false },
                    { 9, 14, true },
                    { 10, 14, false },
                    { 3, 15, true },
                    { 4, 15, true },
                    { 3, 16, true },
                    { 5, 18, true },
                    { 6, 20, false },
                    { 9, 20, true },
                    { 7, 23, true },
                    { 6, 26, false },
                    { 3, 28, true },
                    { 11, 28, true },
                    { 11, 29, true },
                    { 11, 30, true }
                });

            migrationBuilder.CreateIndex(
                name: "i_x_asp_net_role_claims_role_id",
                table: "asp_net_role_claims",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "role_name_index",
                table: "asp_net_roles",
                column: "normalized_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "i_x_asp_net_user_claims_user_id",
                table: "asp_net_user_claims",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "i_x_asp_net_user_logins_user_id",
                table: "asp_net_user_logins",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "i_x_asp_net_user_roles_role_id",
                table: "asp_net_user_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "email_index",
                table: "asp_net_users",
                column: "normalized_email");

            migrationBuilder.CreateIndex(
                name: "user_name_index",
                table: "asp_net_users",
                column: "normalized_user_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "i_x_brands_name",
                table: "brands",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "i_x_categories_parent_id",
                table: "categories",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "i_x_category_attributes_attribute_id",
                table: "category_attributes",
                column: "attribute_id");

            migrationBuilder.CreateIndex(
                name: "i_x_countries_code",
                table: "countries",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "i_x_product_batches_product_id_expiration_date_supply_date",
                table: "product_batches",
                columns: new[] { "product_id", "expiration_date", "supply_date" });

            migrationBuilder.CreateIndex(
                name: "i_x_products_brand_id",
                table: "products",
                column: "brand_id");

            migrationBuilder.CreateIndex(
                name: "i_x_products_category_id",
                table: "products",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "i_x_products_country_id",
                table: "products",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "i_x_products_sku",
                table: "products",
                column: "sku",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "asp_net_role_claims");

            migrationBuilder.DropTable(
                name: "asp_net_user_claims");

            migrationBuilder.DropTable(
                name: "asp_net_user_logins");

            migrationBuilder.DropTable(
                name: "asp_net_user_roles");

            migrationBuilder.DropTable(
                name: "asp_net_user_tokens");

            migrationBuilder.DropTable(
                name: "category_attributes");

            migrationBuilder.DropTable(
                name: "product_batches");

            migrationBuilder.DropTable(
                name: "asp_net_roles");

            migrationBuilder.DropTable(
                name: "asp_net_users");

            migrationBuilder.DropTable(
                name: "attributes");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "brands");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "countries");
        }
    }
}
