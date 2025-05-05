using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JetwaysAdmin.Repositories.Migrations
{
    public partial class sp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "UpdateExistingCustomerProfile",
                table: "Admin_tb_LegalEntity",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "TravelDeskEmail",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<bool>(
                name: "PassGSTDetailsAirline",
                table: "Admin_tb_LegalEntity",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "ParentLegalEntityCode",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifyDate",
                table: "Admin_tb_LegalEntity",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "MobileNumber",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LegalEntityName",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LegalEntityCode",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<bool>(
                name: "IsSEZ",
                table: "Admin_tb_LegalEntity",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "IntegrationRefNumber",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<bool>(
                name: "GSTApplicableManagementFee",
                table: "Admin_tb_LegalEntity",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerBaseCurrency",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerBaseCountry",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Admin_tb_LegalEntity",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<bool>(
                name: "CreateNewGDSProfile",
                table: "Admin_tb_LegalEntity",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "CorporateAccountsCode",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "BussinesEmail",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<string>(
                name: "AssignIATAGroup",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "AddressLine2",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "AddressLine1",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "AccountType",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40);

            migrationBuilder.CreateTable(
                name: "CompanyEmployeeGSTDetails",
                columns: table => new
                {
                    LegalEntityCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LegalEntityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GSTNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GSTName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GSTEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "DashboardSummary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerCount = table.Column<int>(type: "int", nullable: false),
                    SupplierCount = table.Column<int>(type: "int", nullable: false),
                    IATAGroupsCount = table.Column<int>(type: "int", nullable: false),
                    IATACommissionsCount = table.Column<int>(type: "int", nullable: false),
                    MiniFareRulesCount = table.Column<int>(type: "int", nullable: false),
                    ReportingParametersCount = table.Column<int>(type: "int", nullable: false),
                    CabsCount = table.Column<int>(type: "int", nullable: false),
                    BannersCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DashboardSummary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "hierarchyLegalEntity",
                columns: table => new
                {
                    LegalEntityCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentLegalEntityCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "tb_AddNewUser",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LegalEntityId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemIntegrationRef = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApprovalRequiredForBooking = table.Column<bool>(type: "bit", nullable: false),
                    ApprovalRequiredForDeviation = table.Column<bool>(type: "bit", nullable: false),
                    GDSProfileType = table.Column<bool>(type: "bit", nullable: false),
                    ExistingCustomer = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_AddNewUser", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "tb_CustomerAccountBalance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManageAccountBalance = table.Column<bool>(type: "bit", nullable: true),
                    AccountBalanceEntity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BalanceType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OriginalLimit = table.Column<int>(type: "int", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PresentBalance = table.Column<int>(type: "int", nullable: true),
                    AddAmmount = table.Column<int>(type: "int", nullable: true),
                    Createdby = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifyBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AppStatus = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_CustomerAccountBalance", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_CustomerManageStaff",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    key_account_manager = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sales_spoc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Booking_consultant = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Emergency_contact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_group = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LegalEntityCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_CustomerManageStaff", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_CustomersAccountDetail",
                columns: table => new
                {
                    AccountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LegalEntityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IATAGroup = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorporateBookingCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AllowPersonalBooking = table.Column<bool>(type: "bit", nullable: false),
                    PersonalBookingCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PANName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PANNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplyDisplayTDS = table.Column<bool>(type: "bit", nullable: false),
                    TDSRate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentMode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AllowPartialPassthrough = table.Column<bool>(type: "bit", nullable: false),
                    CreditDebitLimit = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_CustomersAccountDetail", x => x.AccountID);
                });

            migrationBuilder.CreateTable(
                name: "tb_IATAGroup",
                columns: table => new
                {
                    GroupID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IATACode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifyByDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_IATAGroup", x => x.GroupID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyEmployeeGSTDetails");

            migrationBuilder.DropTable(
                name: "DashboardSummary");

            migrationBuilder.DropTable(
                name: "hierarchyLegalEntity");

            migrationBuilder.DropTable(
                name: "tb_AddNewUser");

            migrationBuilder.DropTable(
                name: "tb_CustomerAccountBalance");

            migrationBuilder.DropTable(
                name: "tb_CustomerManageStaff");

            migrationBuilder.DropTable(
                name: "tb_CustomersAccountDetail");

            migrationBuilder.DropTable(
                name: "tb_IATAGroup");

            migrationBuilder.AlterColumn<bool>(
                name: "UpdateExistingCustomerProfile",
                table: "Admin_tb_LegalEntity",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TravelDeskEmail",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "PassGSTDetailsAirline",
                table: "Admin_tb_LegalEntity",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ParentLegalEntityCode",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifyDate",
                table: "Admin_tb_LegalEntity",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MobileNumber",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LegalEntityName",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LegalEntityCode",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsSEZ",
                table: "Admin_tb_LegalEntity",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IntegrationRefNumber",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "GSTApplicableManagementFee",
                table: "Admin_tb_LegalEntity",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerBaseCurrency",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerBaseCountry",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Admin_tb_LegalEntity",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "CreateNewGDSProfile",
                table: "Admin_tb_LegalEntity",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CorporateAccountsCode",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BussinesEmail",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AssignIATAGroup",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AddressLine2",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AddressLine1",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AccountType",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40,
                oldNullable: true);
        }
    }
}
