using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevAcademy.Migrations
{
    public partial class AddAdminsTableToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_AdminID",
                table: "Sections",
                column: "AdminID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_AdminID",
                table: "Courses",
                column: "AdminID");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_AdminID",
                table: "Categories",
                column: "AdminID");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Users_AdminID",
                table: "Categories",
                column: "AdminID",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Users_AdminID",
                table: "Courses",
                column: "AdminID",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Users_AdminID",
                table: "Sections",
                column: "AdminID",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Users_AdminID",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Users_AdminID",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Users_AdminID",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_Sections_AdminID",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_Courses_AdminID",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Categories_AdminID",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Users");
        }
    }
}
