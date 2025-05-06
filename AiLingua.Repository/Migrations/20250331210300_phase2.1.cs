using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AiLingua.Repository.Migrations
{
    public partial class phase21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Lessons_AvailableTimeSlotId",
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
                name: "IsBooked",
                table: "AvailableTimeSlots");

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
                name: "IX_Lessons_AvailableTimeSlotId",
                table: "Lessons",
                column: "AvailableTimeSlotId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Lessons_AvailableTimeSlotId",
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

            migrationBuilder.AddColumn<bool>(
                name: "IsBooked",
                table: "AvailableTimeSlots",
                type: "bit",
                nullable: false,
                defaultValue: false);

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
                name: "IX_Lessons_AvailableTimeSlotId",
                table: "Lessons",
                column: "AvailableTimeSlotId",
                unique: true);
        }
    }
}
