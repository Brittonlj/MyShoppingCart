using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyShoppingCart.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedLineItemQty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Claim_Customer_CustomerId",
                table: "Claim");

            migrationBuilder.DropIndex(
                name: "IX_Claim_CustomerId",
                table: "Claim");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "OrderProduct",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "OrderProduct");

            migrationBuilder.CreateIndex(
                name: "IX_Claim_CustomerId",
                table: "Claim",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Claim_Customer_CustomerId",
                table: "Claim",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
