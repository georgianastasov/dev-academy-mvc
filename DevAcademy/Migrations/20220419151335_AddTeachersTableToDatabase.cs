using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevAcademy.Migrations
{
    public partial class AddTeachersTableToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConfirmPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sections_TeacherID",
                table: "Sections",
                column: "TeacherID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TeacherID",
                table: "Courses",
                column: "TeacherID");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_TeacherID",
                table: "Categories",
                column: "TeacherID");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Teachers_TeacherID",
                table: "Categories",
                column: "TeacherID",
                principalTable: "Teachers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Teachers_TeacherID",
                table: "Courses",
                column: "TeacherID",
                principalTable: "Teachers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Teachers_TeacherID",
                table: "Sections",
                column: "TeacherID",
                principalTable: "Teachers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Teachers_TeacherID",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Teachers_TeacherID",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Teachers_TeacherID",
                table: "Sections");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Sections_TeacherID",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_Courses_TeacherID",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Categories_TeacherID",
                table: "Categories");
        }
    }
}
