using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JetwaysAdmin.Repositories.Migrations
{
    public partial class billing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillingEntity",
                table: "CustomerDetails");

            migrationBuilder.DropColumn(
                name: "PresentBalance",
                table: "CustomerDetails");

            migrationBuilder.RenameColumn(
                name: "FrequentflyerNumber",
                table: "tb_CustomersEmployee",
                newName: "UserType");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "tb_InternalUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Logo",
                table: "tb_CustomersEmployee",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BillingEntity",
                columns: table => new
                {
                    BillingEntityCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "tb_EmployeeBillingEntity",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    LegalEntityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillingEntityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Band = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportingManager = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmploymentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CostCenter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserRole = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfJoining = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SystemIntegrationRef = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifyBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AppStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_EmployeeBillingEntity", x => x.OrganizationId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillingEntity");

            migrationBuilder.DropTable(
                name: "tb_EmployeeBillingEntity");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "tb_InternalUsers");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "tb_CustomersEmployee");

            migrationBuilder.RenameColumn(
                name: "UserType",
                table: "tb_CustomersEmployee",
                newName: "FrequentflyerNumber");

            migrationBuilder.AddColumn<string>(
                name: "BillingEntity",
                table: "CustomerDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PresentBalance",
                table: "CustomerDetails",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}
