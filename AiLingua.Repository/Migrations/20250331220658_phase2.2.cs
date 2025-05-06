using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AiLingua.Repository.Migrations
{
    public partial class phase22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_AspNetUsers_UserId",
                table: "Lessons");

            migrationBuilder.DropTable(
                name: "LessonStudent");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_UserId",
                table: "Lessons");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "18df2123-216e-4377-8c64-3c8d3683817b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "708d558c-36de-4701-a42a-4e4b88b91b18");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "86e2c818-ba29-44c0-bb12-bb47808780c8");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Lessons");

            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                table: "Lessons",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_StudentId",
                table: "Lessons",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_AspNetUsers_StudentId",
                table: "Lessons",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_AspNetUsers_StudentId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_StudentId",
                table: "Lessons");

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

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Lessons");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Lessons",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LessonStudent",
                columns: table => new
                {
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonStudent", x => new { x.LessonId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_LessonStudent_AspNetUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonStudent_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "LessonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "18df2123-216e-4377-8c64-3c8d3683817b", "e5945dc3-aff7-4a05-8f38-7e59460d40cb", "Teacher", "TEACHER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "708d558c-36de-4701-a42a-4e4b88b91b18", "475dd2ae-d1fb-4816-8788-6f0d34a8d860", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "86e2c818-ba29-44c0-bb12-bb47808780c8", "a2af811e-47f2-480d-93fe-10cdd282b7e7", "Student", "STUDENT" });

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_UserId",
                table: "Lessons",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonStudent_StudentId",
                table: "LessonStudent",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_AspNetUsers_UserId",
                table: "Lessons",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
