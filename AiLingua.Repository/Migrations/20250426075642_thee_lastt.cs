using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AiLingua.Repository.Migrations
{
    public partial class thee_lastt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5c5c31be-34d7-4266-9f8d-2b004c09a208");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e8f952a-ffe8-411f-b7ec-6c70e35f8ee5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d1dcd3c3-fa11-44e1-a0fc-20c9c9940d61");

            migrationBuilder.AddColumn<string>(
                name: "AccountNumber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "AccountNumber",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5c5c31be-34d7-4266-9f8d-2b004c09a208", "a9d1936e-d7dd-4a9a-a902-2bebe2afe3da", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5e8f952a-ffe8-411f-b7ec-6c70e35f8ee5", "c0d17b1f-d124-4e6a-9fcb-8bbe56618176", "Student", "STUDENT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d1dcd3c3-fa11-44e1-a0fc-20c9c9940d61", "713d99c3-d651-418c-bae6-07c5f9683973", "Teacher", "TEACHER" });
        }
    }
}
