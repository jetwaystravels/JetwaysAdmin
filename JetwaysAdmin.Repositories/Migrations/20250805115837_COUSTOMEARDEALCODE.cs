using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JetwaysAdmin.Repositories.Migrations
{
    public partial class COUSTOMEARDEALCODE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelectedCredentialId",
                table: "tb_SuppliersDealCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SelectedCredentialId",
                table: "tb_SuppliersDealCode",
                type: "int",
                nullable: true);
        }
    }
}
