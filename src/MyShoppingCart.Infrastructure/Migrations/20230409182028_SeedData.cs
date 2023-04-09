using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShoppingCart.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "Id", "City", "PostalCode", "State", "Street" },
                values: new object[,]
                {
                    { new Guid("6b760260-799c-4af1-a173-0bf83a2a74d5"), "Bedrock", "12345", "MO", "123 Test St" },
                    { new Guid("786de95e-2d4c-4524-ac64-6ddf11ad9ec5"), "Bedrock", "12345", "MO", "123 Test St" },
                    { new Guid("b592fa04-541a-4bf2-967c-c07468af2014"), "Space City", "12345", "MO", "123 Test St" },
                    { new Guid("ccb9f54b-f5a0-4d42-927d-c65294e0f629"), "Space City", "12345", "MO", "123 Test St" }
                });

            migrationBuilder.InsertData(
                table: "Claim",
                columns: new[] { "Id", "CustomerId", "Type", "Value" },
                values: new object[,]
                {
                    { new Guid("3809c003-f0b0-4edf-9e1e-c7e2f6746681"), new Guid("79f42c77-83e5-403b-9ec1-6a3ff285c5ac"), "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "79F42C77-83E5-403B-9EC1-6A3FF285C5AC" },
                    { new Guid("5d4e57ca-fffb-431f-947e-0e023ff044c3"), new Guid("79f42c77-83e5-403b-9ec1-6a3ff285c5ac"), "http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "Admin" },
                    { new Guid("62c79539-b9cb-490a-816b-5c2c2c3df77a"), new Guid("4a5eb696-7c8f-47d4-974b-c1da72cec2c5"), "http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "Customer" },
                    { new Guid("ca17ad94-290e-48ec-89fd-b61f0683535f"), new Guid("4a5eb696-7c8f-47d4-974b-c1da72cec2c5"), "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "4A5EB696-7C8F-47D4-974B-C1DA72CEC2C5" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("0553ca62-284d-4379-afc5-c2d4903f7a4c"), "Heavy duty!  Fits most dogs and some people.", null, "A Dog Collar", 100.00m },
                    { new Guid("1caa7fb0-8c2e-4304-a1ec-747a89623131"), "The 80s are calling and they want you back!", null, "Garbage Pale Kids Stickers", 4.00m },
                    { new Guid("24ef70c3-0fc1-48d7-994f-380d4c533419"), "Dark and mysterious!  Good for 2 kids who want to impersonate an adult.", null, "Black Trenchcoat", 100.00m },
                    { new Guid("2df1a80e-651a-417a-9028-b81d30a9a26e"), "The game that destroys friendships and families!", null, "Monopoly", 100.00m },
                    { new Guid("7bc8ae1b-031a-4f3a-815c-2111288ff58c"), "These are some dope Nike Tennis Shoes!", null, "Nike Tennis Shoes", 100.00m },
                    { new Guid("9955f4d7-3e40-4111-a76d-23406f93334b"), "This is some tasty gum, but the flavor doesn't last!", null, "Fruit Stripe Gum", 1.99m },
                    { new Guid("a9c15177-e1a4-4dc8-bcb0-5d78128fdeae"), "Raaawwwrrrr!", null, "Pink Stuffed Dinosaur", 15.99m },
                    { new Guid("ad7d0cf7-ce00-477d-ae2a-5691f65eba0e"), "Cheerios are a healthy part of your breakfast!", null, "Cheerios", 6.00m },
                    { new Guid("e226d6b2-324f-4508-b5e5-0db77b345c69"), "Crisp and clean with no caffeine!", null, "7Up", 1.50m },
                    { new Guid("e3b2bcce-a8f4-4f7e-9c9e-6ac93e03554a"), "The 90s are calling and they want you back!", null, "A Plaid Flannel Shirt", 20.00m }
                });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "BillingAddressId", "Email", "FirstName", "LastName", "ShippingAddressId" },
                values: new object[,]
                {
                    { new Guid("4a5eb696-7c8f-47d4-974b-c1da72cec2c5"), new Guid("786de95e-2d4c-4524-ac64-6ddf11ad9ec5"), "fred.flintstone@test.com", "Fred", "Flintstone", new Guid("6b760260-799c-4af1-a173-0bf83a2a74d5") },
                    { new Guid("79f42c77-83e5-403b-9ec1-6a3ff285c5ac"), new Guid("b592fa04-541a-4bf2-967c-c07468af2014"), "george.jetson@test.com", "George", "Jetson", new Guid("ccb9f54b-f5a0-4d42-927d-c65294e0f629") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Claim",
                keyColumn: "Id",
                keyValue: new Guid("3809c003-f0b0-4edf-9e1e-c7e2f6746681"));

            migrationBuilder.DeleteData(
                table: "Claim",
                keyColumn: "Id",
                keyValue: new Guid("5d4e57ca-fffb-431f-947e-0e023ff044c3"));

            migrationBuilder.DeleteData(
                table: "Claim",
                keyColumn: "Id",
                keyValue: new Guid("62c79539-b9cb-490a-816b-5c2c2c3df77a"));

            migrationBuilder.DeleteData(
                table: "Claim",
                keyColumn: "Id",
                keyValue: new Guid("ca17ad94-290e-48ec-89fd-b61f0683535f"));

            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: new Guid("4a5eb696-7c8f-47d4-974b-c1da72cec2c5"));

            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: new Guid("79f42c77-83e5-403b-9ec1-6a3ff285c5ac"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("0553ca62-284d-4379-afc5-c2d4903f7a4c"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("1caa7fb0-8c2e-4304-a1ec-747a89623131"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("24ef70c3-0fc1-48d7-994f-380d4c533419"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("2df1a80e-651a-417a-9028-b81d30a9a26e"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("7bc8ae1b-031a-4f3a-815c-2111288ff58c"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("9955f4d7-3e40-4111-a76d-23406f93334b"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("a9c15177-e1a4-4dc8-bcb0-5d78128fdeae"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("ad7d0cf7-ce00-477d-ae2a-5691f65eba0e"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("e226d6b2-324f-4508-b5e5-0db77b345c69"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("e3b2bcce-a8f4-4f7e-9c9e-6ac93e03554a"));

            migrationBuilder.DeleteData(
                table: "Address",
                keyColumn: "Id",
                keyValue: new Guid("6b760260-799c-4af1-a173-0bf83a2a74d5"));

            migrationBuilder.DeleteData(
                table: "Address",
                keyColumn: "Id",
                keyValue: new Guid("786de95e-2d4c-4524-ac64-6ddf11ad9ec5"));

            migrationBuilder.DeleteData(
                table: "Address",
                keyColumn: "Id",
                keyValue: new Guid("b592fa04-541a-4bf2-967c-c07468af2014"));

            migrationBuilder.DeleteData(
                table: "Address",
                keyColumn: "Id",
                keyValue: new Guid("ccb9f54b-f5a0-4d42-927d-c65294e0f629"));
        }
    }
}
