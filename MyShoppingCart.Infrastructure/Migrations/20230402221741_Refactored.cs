using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyShoppingCart.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Refactored : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customer_BillingAddressId",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_ShippingAddressId",
                table: "Customer");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_BillingAddressId",
                table: "Customer",
                column: "BillingAddressId",
                unique: true,
                filter: "[BillingAddressId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_ShippingAddressId",
                table: "Customer",
                column: "ShippingAddressId",
                unique: true,
                filter: "[ShippingAddressId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customer_BillingAddressId",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_ShippingAddressId",
                table: "Customer");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_BillingAddressId",
                table: "Customer",
                column: "BillingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_ShippingAddressId",
                table: "Customer",
                column: "ShippingAddressId");
        }
    }
}
