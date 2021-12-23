using Microsoft.EntityFrameworkCore.Migrations;

namespace apiWS.Migrations
{
    public partial class AddRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2483b5a3-c5c6-4bda-8df5-45ab42a839b9", "1ca64e2d-7052-45cd-ad95-8771d6969096", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d1e512b3-d2a5-4662-ab30-507a265138c7", "9ce0f36d-e32c-497a-8b61-1899a6c3a9a7", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2483b5a3-c5c6-4bda-8df5-45ab42a839b9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d1e512b3-d2a5-4662-ab30-507a265138c7");
        }
    }
}
