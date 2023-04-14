using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShoppingCart.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => new { x.ProductId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_ProductCategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCategory_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2478b36b-05f0-4c89-97ab-1a2b8dd0158b"), "Athletic" },
                    { new Guid("31654a1c-14d6-47e5-bdf6-0c158aad9cc9"), "Food" },
                    { new Guid("5202cc15-64bb-4c2b-8bd3-bb9190782a31"), "Cereal" },
                    { new Guid("5330fda9-5934-4d84-936e-7e910ee66cd6"), "Collectable" },
                    { new Guid("592ca9e1-89ee-4807-8070-7e4fe75ce92f"), "Beverage" },
                    { new Guid("663ac0e4-6265-417c-89e8-968b42077169"), "Toy/Game" },
                    { new Guid("a306808a-6156-44b9-8dab-07f05039fa33"), "Pet" },
                    { new Guid("afa2903e-6595-4f62-9bc2-577f0399ce18"), "Gum" },
                    { new Guid("c231f98e-f62e-4672-a12c-18c98b7e7669"), "Clothing" }
                });

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: new Guid("79f42c77-83e5-403b-9ec1-6a3ff285c5ac"),
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAIAAYagAAAAEOt9l5axpZkFW3JyiZaAgn7QpKHgMetmUYNsLd7fovRZV9fvV+OLXPGdNntgfK4pDw==", "HKNQOHKL7CB4G3TH257MX3QYRBOPMIED" });

            migrationBuilder.InsertData(
                table: "ProductCategory",
                columns: new[] { "CategoryId", "ProductId" },
                values: new object[,]
                {
                    { new Guid("a306808a-6156-44b9-8dab-07f05039fa33"), new Guid("0553ca62-284d-4379-afc5-c2d4903f7a4c") },
                    { new Guid("5330fda9-5934-4d84-936e-7e910ee66cd6"), new Guid("1caa7fb0-8c2e-4304-a1ec-747a89623131") },
                    { new Guid("c231f98e-f62e-4672-a12c-18c98b7e7669"), new Guid("24ef70c3-0fc1-48d7-994f-380d4c533419") },
                    { new Guid("663ac0e4-6265-417c-89e8-968b42077169"), new Guid("2df1a80e-651a-417a-9028-b81d30a9a26e") },
                    { new Guid("2478b36b-05f0-4c89-97ab-1a2b8dd0158b"), new Guid("7bc8ae1b-031a-4f3a-815c-2111288ff58c") },
                    { new Guid("afa2903e-6595-4f62-9bc2-577f0399ce18"), new Guid("9955f4d7-3e40-4111-a76d-23406f93334b") },
                    { new Guid("663ac0e4-6265-417c-89e8-968b42077169"), new Guid("a9c15177-e1a4-4dc8-bcb0-5d78128fdeae") },
                    { new Guid("31654a1c-14d6-47e5-bdf6-0c158aad9cc9"), new Guid("ad7d0cf7-ce00-477d-ae2a-5691f65eba0e") },
                    { new Guid("5202cc15-64bb-4c2b-8bd3-bb9190782a31"), new Guid("ad7d0cf7-ce00-477d-ae2a-5691f65eba0e") },
                    { new Guid("592ca9e1-89ee-4807-8070-7e4fe75ce92f"), new Guid("e226d6b2-324f-4508-b5e5-0db77b345c69") },
                    { new Guid("c231f98e-f62e-4672-a12c-18c98b7e7669"), new Guid("e3b2bcce-a8f4-4f7e-9c9e-6ac93e03554a") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_CategoryId",
                table: "ProductCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_ProductId",
                table: "ProductCategory",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductCategory");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: new Guid("79f42c77-83e5-403b-9ec1-6a3ff285c5ac"),
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAIAAYagAAAAEDvfuUmbZTWsI9Xgb//t60GssHdXbjTzIh7MIxZ6FGCRjcWIQs14ZCMXjkuYKetxKA==", "T5MUNAWWSMQHJTKUYXXI35K2OQ6O4Q7D" });
        }
    }
}
