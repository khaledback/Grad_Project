using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AiLingua.Repository.Migrations
{
    public partial class phase2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                keyValue: "12df9d40-dfe8-42e9-995b-0e7e956f4795");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3e41133c-1269-49d5-916e-43f3d4397c82");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Lessons");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Lessons",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CountOfStudents",
                table: "AvailableTimeSlots",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "price",
                table: "AvailableTimeSlots",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

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
                values: new object[] { "b173ada8-40ee-4a1b-97d0-8a5771b1be1c", "78d8a83e-d827-4366-acc3-a180c6b36418", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b3d562ff-0b1f-4ecb-a6f1-bc49db35b6c9", "3fe8fd41-5261-49c5-825b-db356b76bce9", "Teacher", "TEACHER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "efa414bc-6c47-4221-89d9-e1984a74f38d", "97e11914-d649-4596-9ac5-3eeb2fc8c648", "Student", "STUDENT" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                keyValue: "b173ada8-40ee-4a1b-97d0-8a5771b1be1c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b3d562ff-0b1f-4ecb-a6f1-bc49db35b6c9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "efa414bc-6c47-4221-89d9-e1984a74f38d");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "CountOfStudents",
                table: "AvailableTimeSlots");

            migrationBuilder.DropColumn(
                name: "price",
                table: "AvailableTimeSlots");

            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                table: "Lessons",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "12df9d40-dfe8-42e9-995b-0e7e956f4795", "1884c0e9-7de2-4b26-9ba2-a60e9964795d", "Teacher", "TEACHER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3e41133c-1269-49d5-916e-43f3d4397c82", "e43b3855-e582-4db6-875b-86245dbedcc4", "Student", "STUDENT" });

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
    }
}
