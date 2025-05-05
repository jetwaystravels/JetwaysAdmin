using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JetwaysAdmin.Repositories.Migrations
{
    public partial class IataGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MobileNumber",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MobileNumber",
                table: "Admin_tb_LegalEntity",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
