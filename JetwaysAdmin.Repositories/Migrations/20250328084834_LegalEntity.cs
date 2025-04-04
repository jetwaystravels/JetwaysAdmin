using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JetwaysAdmin.Repositories.Migrations
{
    public partial class LegalEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin_tb_LegalEntity",
                columns: table => new
                {
                    LegalEntityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LegalEntityName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LegalEntityCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ParentLegalEntityCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AssignIATAGroup = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CorporateAccountsCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ManageAccountBalance = table.Column<bool>(type: "bit", nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AddressLine2 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IntegrationRefNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GSTApplicableManagementFee = table.Column<bool>(type: "bit", nullable: false),
                    PassGSTDetailsAirline = table.Column<bool>(type: "bit", nullable: false),
                    IsSEZ = table.Column<bool>(type: "bit", nullable: false),
                    CustomerBaseCurrency = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Logo = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AppStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin_tb_LegalEntity", x => x.LegalEntityID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin_tb_LegalEntity");
        }
    }
}
