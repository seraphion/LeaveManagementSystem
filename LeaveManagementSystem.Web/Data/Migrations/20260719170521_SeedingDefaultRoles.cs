using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeaveManagementSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDefaultRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9156a4db-70fe-4e43-92bd-0d7aa6c8468d", "eec1e307-bcc4-4671-b88b-7c25b6c603a8", "Administrator", "ADMINISTRATOR" },
                    { "92d294e3-ed18-4f51-b59a-3b30545a4780", "dfbc0ceb-64af-4625-9ae7-4caf432b203a", "Supervisor", "SUPERVISOR" },
                    { "a209a3c1-93b3-44c8-aa97-933ff0a6dd24", "b8b3c98f-249e-49bf-9187-628419c01c4e", "Employee", "EMPLOYEE" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9156a4db-70fe-4e43-92bd-0d7aa6c8468d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "92d294e3-ed18-4f51-b59a-3b30545a4780");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a209a3c1-93b3-44c8-aa97-933ff0a6dd24");
        }
    }
}
