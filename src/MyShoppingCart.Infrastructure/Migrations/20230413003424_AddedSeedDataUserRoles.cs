using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShoppingCart.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedSeedDataUserRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("357b00a9-eb69-4632-da13-08db3ba8f22d"), new Guid("4a5eb696-7c8f-47d4-974b-c1da72cec2c5") },
                    { new Guid("28ca8ce6-cf1d-42b5-da12-08db3ba8f22d"), new Guid("79f42c77-83e5-403b-9ec1-6a3ff285c5ac") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("357b00a9-eb69-4632-da13-08db3ba8f22d"), new Guid("4a5eb696-7c8f-47d4-974b-c1da72cec2c5") });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("28ca8ce6-cf1d-42b5-da12-08db3ba8f22d"), new Guid("79f42c77-83e5-403b-9ec1-6a3ff285c5ac") });
        }
    }
}
