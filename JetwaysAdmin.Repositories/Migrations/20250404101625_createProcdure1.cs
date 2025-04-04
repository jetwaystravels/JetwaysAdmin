using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JetwaysAdmin.Repositories.Migrations
{
    public partial class createProcdure1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Admin_tb_LegalEntity");

            migrationBuilder.CreateTable(
                name: "CustomerDetails",
                columns: table => new
                {
                    LegalEntityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeNames = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerDetails");

            migrationBuilder.AddColumn<byte[]>(
                name: "Logo",
                table: "Admin_tb_LegalEntity",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
