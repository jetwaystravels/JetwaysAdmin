using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JetwaysAdmin.Repositories.Migrations
{
    public partial class updategit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmailId",
                table: "tb_AddNewUser",
                newName: "EmplyId");

            migrationBuilder.RenameColumn(
                name: "EmployeeID",
                table: "CompanyEmployeeGSTDetails",
                newName: "FrequentFlyerNumbers");

            migrationBuilder.AddColumn<bool>(
                name: "AppStatus",
                table: "tb_AddNewUser",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "ParentLegalEntityCode",
                table: "hierarchyLegalEntity",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Level",
                table: "hierarchyLegalEntity",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "hierarchyLegalEntity",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LegalEntityName",
                table: "hierarchyLegalEntity",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "PresentBalance",
                table: "CustomerDetails",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeNames",
                table: "CustomerDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "tb_CustomersEmployee",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LegalEntityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileCountryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FrequentflyerNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SystemIntegrationRefNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovalRequiredForBooking = table.Column<bool>(type: "bit", nullable: true),
                    ApprovalRequiredForDeviation = table.Column<bool>(type: "bit", nullable: true),
                    GDSProfileType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifyBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AppStatus = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_CustomersEmployee", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "tb_EmployeeFrequentFlyers",
                columns: table => new
                {
                    FrequentFlyerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<int>(type: "int", nullable: true),
                    AirlineID = table.Column<int>(type: "int", nullable: true),
                    FrequentFlyerNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MembershipLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_EmployeeFrequentFlyers", x => x.FrequentFlyerID);
                });

            migrationBuilder.CreateTable(
                name: "tb_SuppliersDetail",
                columns: table => new
                {
                    SupplierId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarrierType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PinCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierEmails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    SendAmendmentNotifications = table.Column<bool>(type: "bit", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppStatus = table.Column<bool>(type: "bit", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Domain = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlightCode = table.Column<int>(type: "int", nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_SuppliersDetail", x => x.SupplierId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_CustomersEmployee");

            migrationBuilder.DropTable(
                name: "tb_EmployeeFrequentFlyers");

            migrationBuilder.DropTable(
                name: "tb_SuppliersDetail");

            migrationBuilder.DropColumn(
                name: "AppStatus",
                table: "tb_AddNewUser");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "hierarchyLegalEntity");

            migrationBuilder.DropColumn(
                name: "LegalEntityName",
                table: "hierarchyLegalEntity");

            migrationBuilder.RenameColumn(
                name: "EmplyId",
                table: "tb_AddNewUser",
                newName: "EmailId");

            migrationBuilder.RenameColumn(
                name: "FrequentFlyerNumbers",
                table: "CompanyEmployeeGSTDetails",
                newName: "EmployeeID");

            migrationBuilder.AlterColumn<string>(
                name: "ParentLegalEntityCode",
                table: "hierarchyLegalEntity",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Level",
                table: "hierarchyLegalEntity",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PresentBalance",
                table: "CustomerDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeNames",
                table: "CustomerDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
