using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevAcademy.Migrations
{
    public partial class UpdateAdminsTableToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "Admins",
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
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Admins_AdminID",
                table: "Categories",
                column: "AdminID",
                principalTable: "Admins",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Admins_AdminID",
                table: "Courses",
                column: "AdminID",
                principalTable: "Admins",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Admins_AdminID",
                table: "Sections",
                column: "AdminID",
                principalTable: "Admins",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Admins_AdminID",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Admins_AdminID",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Admins_AdminID",
                table: "Sections");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
    }
}
