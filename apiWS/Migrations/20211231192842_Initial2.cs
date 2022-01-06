using Microsoft.EntityFrameworkCore.Migrations;

namespace apiWS.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "08179495-868f-423d-95b9-c9759f169582");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "20dd9a60-a746-4f7b-9960-4250eae61412");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a3946ea7-cfa9-4573-92d1-cfeaa5b1dc06", "0c7983ba-b5c5-4a83-bc85-9bcae8d8c585", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "eb4443c3-cf42-4813-bfc5-caef7429cb13", "bd1ed228-fa86-4d7c-b979-a5e8b1faf281", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a3946ea7-cfa9-4573-92d1-cfeaa5b1dc06");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eb4443c3-cf42-4813-bfc5-caef7429cb13");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "08179495-868f-423d-95b9-c9759f169582", "b5477829-6b02-46da-99cd-88afad4e9202", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "20dd9a60-a746-4f7b-9960-4250eae61412", "cd70c7eb-3a66-45f7-bd03-101b5f842de5", "Administrator", "ADMINISTRATOR" });
        }
    }
}
