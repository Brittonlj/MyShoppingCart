using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShoppingCart.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedIdentityToCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserClaim_User_UserId",
                table: "UserClaim");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLogin_User_UserId",
                table: "UserLogin");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_User_UserId",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserToken_User_UserId",
                table: "UserToken");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DeleteData(
                table: "Claim",
                keyColumn: "Id",
                keyValue: new Guid("20302cca-2fc4-494e-bfad-b701a7b156f9"));

            migrationBuilder.DeleteData(
                table: "Claim",
                keyColumn: "Id",
                keyValue: new Guid("756775dd-f1b3-4719-b9a9-8e5ead396933"));

            migrationBuilder.DeleteData(
                table: "Claim",
                keyColumn: "Id",
                keyValue: new Guid("c618626c-5f06-4059-893e-3ecece30dc28"));

            migrationBuilder.DeleteData(
                table: "Claim",
                keyColumn: "Id",
                keyValue: new Guid("d2619c52-11b0-4c2b-8f03-f158b3a65c2a"));

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "Customer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Customer",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "Customer",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "Customer",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "Customer",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "Customer",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "Customer",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "Customer",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Customer",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

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
                columns: new[] { "AccessFailedCount", "ConcurrencyStamp", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 0, "e650311e-8a38-4c6b-a0fc-13c99b4f70da", false, false, null, null, null, null, null, false, null, false, null });

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: new Guid("79f42c77-83e5-403b-9ec1-6a3ff285c5ac"),
                columns: new[] { "AccessFailedCount", "ConcurrencyStamp", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 0, "2cb4f00c-1f86-4924-a965-a42e55067f1f", false, false, null, null, null, null, null, false, null, false, null });

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Customer",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Customer",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_UserClaim_Customer_UserId",
                table: "UserClaim",
                column: "UserId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogin_Customer_UserId",
                table: "UserLogin",
                column: "UserId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Customer_UserId",
                table: "UserRole",
                column: "UserId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserToken_Customer_UserId",
                table: "UserToken",
                column: "UserId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserClaim_Customer_UserId",
                table: "UserClaim");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLogin_Customer_UserId",
                table: "UserLogin");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Customer_UserId",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserToken_Customer_UserId",
                table: "UserToken");

            migrationBuilder.DropIndex(
                name: "EmailIndex",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "Customer");

            migrationBuilder.DeleteData(
                table: "Claim",
                keyColumn: "Id",
                keyValue: new Guid("1ec4abf1-2427-4971-ac32-69fc9e3034d7"));

            migrationBuilder.DeleteData(
                table: "Claim",
                keyColumn: "Id",
                keyValue: new Guid("7da154b4-0861-416c-a1c2-445eb99f4b27"));

            migrationBuilder.DeleteData(
                table: "Claim",
                keyColumn: "Id",
                keyValue: new Guid("cb359c6c-4bb7-41e5-96b6-947bfb03f232"));

            migrationBuilder.DeleteData(
                table: "Claim",
                keyColumn: "Id",
                keyValue: new Guid("f56e3465-0de3-41c7-a855-9e3be6862896"));

            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Customer");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.InsertData(
                table: "Claim",
                columns: new[] { "Id", "CustomerId", "Type", "Value" },
                values: new object[,]
                {
                    { new Guid("20302cca-2fc4-494e-bfad-b701a7b156f9"), new Guid("4a5eb696-7c8f-47d4-974b-c1da72cec2c5"), "http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "Customer" },
                    { new Guid("756775dd-f1b3-4719-b9a9-8e5ead396933"), new Guid("4a5eb696-7c8f-47d4-974b-c1da72cec2c5"), "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "4A5EB696-7C8F-47D4-974B-C1DA72CEC2C5" },
                    { new Guid("c618626c-5f06-4059-893e-3ecece30dc28"), new Guid("79f42c77-83e5-403b-9ec1-6a3ff285c5ac"), "http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "Admin" },
                    { new Guid("d2619c52-11b0-4c2b-8f03-f158b3a65c2a"), new Guid("79f42c77-83e5-403b-9ec1-6a3ff285c5ac"), "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "79F42C77-83E5-403B-9EC1-6A3FF285C5AC" }
                });

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "User",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "User",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_UserClaim_User_UserId",
                table: "UserClaim",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogin_User_UserId",
                table: "UserLogin",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_User_UserId",
                table: "UserRole",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserToken_User_UserId",
                table: "UserToken",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
