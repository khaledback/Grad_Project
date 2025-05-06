using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AiLingua.Repository.Migrations
{
    public partial class llastt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "00fe9b1b-e9cc-488a-b923-c522480a835d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1ee26dc5-1f4d-4885-9331-5f9e0916d528");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fa446b8-21e5-46ae-869e-dfb9b453b6ad");

            migrationBuilder.AlterColumn<string>(
                name: "AccountNumber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3c9ec295-53c3-4627-8760-79f5cea80fc2", "67525136-86be-432d-ac37-aac997ca1c45", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a1787cbc-9eae-439b-953e-05aeadb50bf2", "16e4fc15-15c0-44dd-8a6e-669e9a295114", "Student", "STUDENT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c6547f53-dd53-47fb-99e2-9f9246a12f22", "18aa52c3-33f4-4b45-9cc1-4c69cf96d354", "Teacher", "TEACHER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c9ec295-53c3-4627-8760-79f5cea80fc2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a1787cbc-9eae-439b-953e-05aeadb50bf2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c6547f53-dd53-47fb-99e2-9f9246a12f22");

            migrationBuilder.AlterColumn<string>(
                name: "AccountNumber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "00fe9b1b-e9cc-488a-b923-c522480a835d", "8ba7e1e8-8583-4cdc-93a9-22f06ed3ed02", "Student", "STUDENT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1ee26dc5-1f4d-4885-9331-5f9e0916d528", "af75f726-f4e3-4721-896f-ae7d754cbdf3", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7fa446b8-21e5-46ae-869e-dfb9b453b6ad", "a820f4d3-50a5-48c8-b439-74ca247d84dc", "Teacher", "TEACHER" });
        }
    }
}
