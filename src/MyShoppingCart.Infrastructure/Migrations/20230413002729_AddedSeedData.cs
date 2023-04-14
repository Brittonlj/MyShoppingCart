using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShoppingCart.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Claim");

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: new Guid("4a5eb696-7c8f-47d4-974b-c1da72cec2c5"),
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "998eaff7-78dd-4efc-a16e-5a23a4053398", true, "FRED.FLINTSTONE@TEST.COM", "FRED.FLINTSTONE", "AQAAAAIAAYagAAAAEKy9NjMnPf12p4csSvLOiAmdC5zHZnhaF1DkgGH7+e9im6aIuYftYn/cqQP5qgDgLA==", "DVI25ATQSEVFM2MVEPL45HBEWPLT6DNG", "fred.flintstone" });

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: new Guid("79f42c77-83e5-403b-9ec1-6a3ff285c5ac"),
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "02fe5045-5c7b-4641-90a0-e9c5d375fb7b", true, "GEORGE.JETSON@TEST.COM", "GEORGE.JETSON", "AQAAAAIAAYagAAAAEOt9l5axpZkFW3JyiZaAgn7QpKHgMetmUYNsLd7fovRZV9fvV+OLXPGdNntgfK4pDw==", "HKNQOHKL7CB4G3TH257MX3QYRBOPMIED", "george.jetson" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("28ca8ce6-cf1d-42b5-da12-08db3ba8f22d"), null, "Admin", "ADMIN" },
                    { new Guid("357b00a9-eb69-4632-da13-08db3ba8f22d"), null, "Customer", "CUSTOMER" }
                });

            migrationBuilder.InsertData(
                table: "UserClaim",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[,]
                {
                    { 1, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "4A5EB696-7C8F-47D4-974B-C1DA72CEC2C5", new Guid("4a5eb696-7c8f-47d4-974b-c1da72cec2c5") },
                    { 2, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "79F42C77-83E5-403B-9EC1-6A3FF285C5AC", new Guid("79f42c77-83e5-403b-9ec1-6a3ff285c5ac") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("28ca8ce6-cf1d-42b5-da12-08db3ba8f22d"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("357b00a9-eb69-4632-da13-08db3ba8f22d"));

            migrationBuilder.DeleteData(
                table: "UserClaim",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserClaim",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.CreateTable(
                name: "Claim",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claim", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.InsertData(
                table: "Claim",
                columns: new[] { "Id", "CustomerId", "Type", "Value" },
                values: new object[,]
                {
                    { new Guid("1ec4abf1-2427-4971-ac32-69fc9e3034d7"), new Guid("79f42c77-83e5-403b-9ec1-6a3ff285c5ac"), "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "79F42C77-83E5-403B-9EC1-6A3FF285C5AC" },
                    { new Guid("7da154b4-0861-416c-a1c2-445eb99f4b27"), new Guid("4a5eb696-7c8f-47d4-974b-c1da72cec2c5"), "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "4A5EB696-7C8F-47D4-974B-C1DA72CEC2C5" },
                    { new Guid("cb359c6c-4bb7-41e5-96b6-947bfb03f232"), new Guid("4a5eb696-7c8f-47d4-974b-c1da72cec2c5"), "http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "Customer" },
                    { new Guid("f56e3465-0de3-41c7-a855-9e3be6862896"), new Guid("79f42c77-83e5-403b-9ec1-6a3ff285c5ac"), "http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "Admin" }
                });

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: new Guid("4a5eb696-7c8f-47d4-974b-c1da72cec2c5"),
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "e650311e-8a38-4c6b-a0fc-13c99b4f70da", false, null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: new Guid("79f42c77-83e5-403b-9ec1-6a3ff285c5ac"),
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "2cb4f00c-1f86-4924-a965-a42e55067f1f", false, null, null, null, null, null });
        }
    }
}
