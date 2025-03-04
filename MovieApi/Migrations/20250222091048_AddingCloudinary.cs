using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieApi.Migrations
{
    /// <inheritdoc />
    public partial class AddingCloudinary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "59d8fb85-25ec-4b90-a2a4-baec24cdf671");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9ad6cb3f-7b90-49f0-b224-c560034eed7c");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "57e2a745-ff7d-46c6-91ad-50e05efc0758", null, "User", "USER" },
                    { "95258178-d2c5-40ee-bd28-70cde343141e", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "57e2a745-ff7d-46c6-91ad-50e05efc0758");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "95258178-d2c5-40ee-bd28-70cde343141e");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Movies");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "59d8fb85-25ec-4b90-a2a4-baec24cdf671", null, "User", "USER" },
                    { "9ad6cb3f-7b90-49f0-b224-c560034eed7c", null, "Admin", "ADMIN" }
                });
        }
    }
}
