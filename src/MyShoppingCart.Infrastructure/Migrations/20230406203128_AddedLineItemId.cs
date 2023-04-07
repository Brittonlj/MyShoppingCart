using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyShoppingCart.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedLineItemId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LineItem",
                table: "LineItem");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "LineItem",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_LineItem",
                table: "LineItem",
                column: "Id")
                .Annotation("SqlServer:Clustered", true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LineItem",
                table: "LineItem");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "LineItem");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LineItem",
                table: "LineItem",
                columns: new[] { "OrderId", "ProductId" })
                .Annotation("SqlServer:Clustered", true);
        }
    }
}
