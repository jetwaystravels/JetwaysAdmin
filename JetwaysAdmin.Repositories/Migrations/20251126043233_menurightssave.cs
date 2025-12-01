using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JetwaysAdmin.Repositories.Migrations
{
    public partial class menurightssave : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Booking_consultant",
                table: "tb_CustomerManageStaff",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "DepartmentCode",
                table: "tb_CustomerDesignation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BookingConsultantNames",
                table: "BookingConsultants",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Emergency_contact",
                table: "BookingConsultants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KeyAccountManagerNames",
                table: "BookingConsultants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SalesSpocNames",
                table: "BookingConsultants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "User_group",
                table: "BookingConsultants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OfficeType",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Admin_tb_LegalEntityDB",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfficeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LegalEntityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LegalEntityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinancialType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentLegalEntityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GSTLocationID = table.Column<int>(type: "int", nullable: true),
                    FinancialPersonalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignIATAGroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CorporateAccountsCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BussinesEmail = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    AddressLine1 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AccountType = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    IntegrationRefNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    GSTApplicableManagementFee = table.Column<bool>(type: "bit", nullable: true),
                    PassGSTDetailsAirline = table.Column<bool>(type: "bit", nullable: true),
                    IsSEZ = table.Column<bool>(type: "bit", nullable: true),
                    CustomerBaseCurrency = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CustomerBaseCountry = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TravelDeskEmail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AcountActivationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AccountDeactivationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateNewGDSProfile = table.Column<bool>(type: "bit", nullable: true),
                    UpdateExistingCustomerProfile = table.Column<bool>(type: "bit", nullable: true),
                    Createdby = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifyBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AppStatus = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin_tb_LegalEntityDB", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tb_MenuRights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MenuId = table.Column<int>(type: "int", nullable: false),
                    CanView = table.Column<bool>(type: "bit", nullable: false),
                    CanAdd = table.Column<bool>(type: "bit", nullable: false),
                    CanEdit = table.Column<bool>(type: "bit", nullable: false),
                    CanDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_MenuRights", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin_tb_LegalEntityDB");

            migrationBuilder.DropTable(
                name: "Tb_MenuRights");

            migrationBuilder.DropColumn(
                name: "DepartmentCode",
                table: "tb_CustomerDesignation");

            migrationBuilder.DropColumn(
                name: "Emergency_contact",
                table: "BookingConsultants");

            migrationBuilder.DropColumn(
                name: "KeyAccountManagerNames",
                table: "BookingConsultants");

            migrationBuilder.DropColumn(
                name: "SalesSpocNames",
                table: "BookingConsultants");

            migrationBuilder.DropColumn(
                name: "User_group",
                table: "BookingConsultants");

            migrationBuilder.DropColumn(
                name: "OfficeType",
                table: "Admin_tb_LegalEntity");

            migrationBuilder.AlterColumn<string>(
                name: "Booking_consultant",
                table: "tb_CustomerManageStaff",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BookingConsultantNames",
                table: "BookingConsultants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
