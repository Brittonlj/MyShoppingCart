using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShoppingCart.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedIdentityFramework : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaim_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaim_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogin",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogin_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToken",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserToken_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("0553ca62-284d-4379-afc5-c2d4903f7a4c"),
                columns: new[] { "Description", "ImageUrl" },
                values: new object[] { "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis. Nec ullamcorper sit amet risus nullam eget felis eget nunc. Sapien eget mi proin sed libero enim. Nam at lectus urna duis. Volutpat maecenas volutpat blandit aliquam.", "/img/dog_collar.jpg" });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("1caa7fb0-8c2e-4304-a1ec-747a89623131"),
                column: "Description",
                value: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis. Nec ullamcorper sit amet risus nullam eget felis eget nunc. Sapien eget mi proin sed libero enim. Nam at lectus urna duis. Volutpat maecenas volutpat blandit aliquam.");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("24ef70c3-0fc1-48d7-994f-380d4c533419"),
                column: "Description",
                value: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis. Nec ullamcorper sit amet risus nullam eget felis eget nunc. Sapien eget mi proin sed libero enim. Nam at lectus urna duis. Volutpat maecenas volutpat blandit aliquam.");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("2df1a80e-651a-417a-9028-b81d30a9a26e"),
                column: "Description",
                value: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis. Nec ullamcorper sit amet risus nullam eget felis eget nunc. Sapien eget mi proin sed libero enim. Nam at lectus urna duis. Volutpat maecenas volutpat blandit aliquam.");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("7bc8ae1b-031a-4f3a-815c-2111288ff58c"),
                column: "Description",
                value: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis. Nec ullamcorper sit amet risus nullam eget felis eget nunc. Sapien eget mi proin sed libero enim. Nam at lectus urna duis. Volutpat maecenas volutpat blandit aliquam.");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("9955f4d7-3e40-4111-a76d-23406f93334b"),
                column: "Description",
                value: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis. Nec ullamcorper sit amet risus nullam eget felis eget nunc. Sapien eget mi proin sed libero enim. Nam at lectus urna duis. Volutpat maecenas volutpat blandit aliquam.");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("a9c15177-e1a4-4dc8-bcb0-5d78128fdeae"),
                column: "Description",
                value: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis. Nec ullamcorper sit amet risus nullam eget felis eget nunc. Sapien eget mi proin sed libero enim. Nam at lectus urna duis. Volutpat maecenas volutpat blandit aliquam.");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("ad7d0cf7-ce00-477d-ae2a-5691f65eba0e"),
                column: "Description",
                value: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis. Nec ullamcorper sit amet risus nullam eget felis eget nunc. Sapien eget mi proin sed libero enim. Nam at lectus urna duis. Volutpat maecenas volutpat blandit aliquam.");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("e226d6b2-324f-4508-b5e5-0db77b345c69"),
                column: "Description",
                value: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis. Nec ullamcorper sit amet risus nullam eget felis eget nunc. Sapien eget mi proin sed libero enim. Nam at lectus urna duis. Volutpat maecenas volutpat blandit aliquam.");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("e3b2bcce-a8f4-4f7e-9c9e-6ac93e03554a"),
                column: "Description",
                value: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis. Nec ullamcorper sit amet risus nullam eget felis eget nunc. Sapien eget mi proin sed libero enim. Nam at lectus urna duis. Volutpat maecenas volutpat blandit aliquam.");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Role",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaim_RoleId",
                table: "RoleClaim",
                column: "RoleId");

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

            migrationBuilder.CreateIndex(
                name: "IX_UserClaim_UserId",
                table: "UserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogin_UserId",
                table: "UserLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleClaim");

            migrationBuilder.DropTable(
                name: "UserClaim");

            migrationBuilder.DropTable(
                name: "UserLogin");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "UserToken");

            migrationBuilder.DropTable(
                name: "Role");

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

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("0553ca62-284d-4379-afc5-c2d4903f7a4c"),
                columns: new[] { "Description", "ImageUrl" },
                values: new object[] { "Heavy duty!  Fits most dogs and some people.", null });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("1caa7fb0-8c2e-4304-a1ec-747a89623131"),
                column: "Description",
                value: "The 80s are calling and they want you back!");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("24ef70c3-0fc1-48d7-994f-380d4c533419"),
                column: "Description",
                value: "Dark and mysterious!  Good for 2 kids who want to impersonate an adult.");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("2df1a80e-651a-417a-9028-b81d30a9a26e"),
                column: "Description",
                value: "The game that destroys friendships and families!");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("7bc8ae1b-031a-4f3a-815c-2111288ff58c"),
                column: "Description",
                value: "These are some dope Nike Tennis Shoes!");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("9955f4d7-3e40-4111-a76d-23406f93334b"),
                column: "Description",
                value: "This is some tasty gum, but the flavor doesn't last!");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("a9c15177-e1a4-4dc8-bcb0-5d78128fdeae"),
                column: "Description",
                value: "Raaawwwrrrr!");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("ad7d0cf7-ce00-477d-ae2a-5691f65eba0e"),
                column: "Description",
                value: "Cheerios are a healthy part of your breakfast!");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("e226d6b2-324f-4508-b5e5-0db77b345c69"),
                column: "Description",
                value: "Crisp and clean with no caffeine!");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("e3b2bcce-a8f4-4f7e-9c9e-6ac93e03554a"),
                column: "Description",
                value: "The 90s are calling and they want you back!");
        }
    }
}
