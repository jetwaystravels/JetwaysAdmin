using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JetwaysAdmin.Repositories.Migrations
{
    public partial class status : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Domain",
                table: "tb_SuppliersDetail");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "tb_SuppliersDetail");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "tb_SuppliersDetail");

            migrationBuilder.AddColumn<string>(
                name: "LegalEntityCode",
                table: "tb_EmployeeFrequentFlyers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeID",
                table: "tb_CustomersEmployee",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TDSRate",
                table: "tb_CustomersAccountDetail",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PersonalBookingCode",
                table: "tb_CustomersAccountDetail",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentMode",
                table: "tb_CustomersAccountDetail",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PANNumber",
                table: "tb_CustomersAccountDetail",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PANName",
                table: "tb_CustomersAccountDetail",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LegalEntityCode",
                table: "tb_CustomersAccountDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "CreditDebitLimit",
                table: "tb_CustomersAccountDetail",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CorporateBookingCode",
                table: "tb_CustomersAccountDetail",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "ApplyDisplayTDS",
                table: "tb_CustomersAccountDetail",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "AllowPersonalBooking",
                table: "tb_CustomersAccountDetail",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "AllowPartialPassthrough",
                table: "tb_CustomersAccountDetail",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "key_account_manager",
                table: "tb_CustomerManageStaff",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "User_group",
                table: "tb_CustomerManageStaff",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Sales_spoc",
                table: "tb_CustomerManageStaff",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Emergency_contact",
                table: "tb_CustomerManageStaff",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "BookingConsultants",
                columns: table => new
                {
                    LegalEntityCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookingConsultantNames = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "tb_City",
                columns: table => new
                {
                    CityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateID = table.Column<int>(type: "int", nullable: false),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_City", x => x.CityID);
                });

            migrationBuilder.CreateTable(
                name: "tb_Country",
                columns: table => new
                {
                    CountryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Country", x => x.CountryID);
                });

            migrationBuilder.CreateTable(
                name: "tb_CustomerDealCodes",
                columns: table => new
                {
                    DealCodeID = table.Column<int>(type: "int", nullable: false),
                    LegalEntityCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DealCodeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SelectedCredential = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TravelType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CabinClass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DealPricingCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TourCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DealCodeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassOfSeats = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GstMandatory = table.Column<bool>(type: "bit", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookingType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "tb_CustomerLocationTaxDetails",
                columns: table => new
                {
                    LocationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LegalEntityCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(18,10)", nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(18,10)", nullable: true),
                    GSTRegistered = table.Column<bool>(type: "bit", nullable: true),
                    UINRegistered = table.Column<bool>(type: "bit", nullable: true),
                    GSTNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GSTName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GSTEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileCountryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonalBooking = table.Column<bool>(type: "bit", nullable: true),
                    IsSEZ = table.Column<bool>(type: "bit", nullable: true),
                    LocationHead = table.Column<int>(type: "int", nullable: true),
                    LocationManager1 = table.Column<int>(type: "int", nullable: true),
                    LocationManager2 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_CustomerLocationTaxDetails", x => x.LocationID);
                });

            migrationBuilder.CreateTable(
                name: "tb_DealCode",
                columns: table => new
                {
                    DealCodeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PCC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DealCodeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SelectedCredentialId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TravelMode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DealPricingCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TourCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssociatedFareTypes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CabinClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClassOfSeats = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AutoEnableDealCode = table.Column<bool>(type: "bit", nullable: true),
                    GSTMandatory = table.Column<bool>(type: "bit", nullable: true),
                    OverrideCustomerGST = table.Column<bool>(type: "bit", nullable: true),
                    BookingType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_DealCode", x => x.DealCodeId);
                });

            migrationBuilder.CreateTable(
                name: "tb_State",
                columns: table => new
                {
                    StateID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryID = table.Column<int>(type: "int", nullable: false),
                    StateName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_State", x => x.StateID);
                });

            migrationBuilder.CreateTable(
                name: "tb_SuppliersCredential",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CredentialsName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganizationId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssociatedFareTypes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TravelType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CredentialsType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Img_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_SuppliersCredential", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingConsultants");

            migrationBuilder.DropTable(
                name: "tb_City");

            migrationBuilder.DropTable(
                name: "tb_Country");

            migrationBuilder.DropTable(
                name: "tb_CustomerDealCodes");

            migrationBuilder.DropTable(
                name: "tb_CustomerLocationTaxDetails");

            migrationBuilder.DropTable(
                name: "tb_DealCode");

            migrationBuilder.DropTable(
                name: "tb_State");

            migrationBuilder.DropTable(
                name: "tb_SuppliersCredential");

            migrationBuilder.DropColumn(
                name: "LegalEntityCode",
                table: "tb_EmployeeFrequentFlyers");

            migrationBuilder.AddColumn<string>(
                name: "Domain",
                table: "tb_SuppliersDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "tb_SuppliersDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "tb_SuppliersDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeID",
                table: "tb_CustomersEmployee",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "TDSRate",
                table: "tb_CustomersAccountDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PersonalBookingCode",
                table: "tb_CustomersAccountDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PaymentMode",
                table: "tb_CustomersAccountDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PANNumber",
                table: "tb_CustomersAccountDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PANName",
                table: "tb_CustomersAccountDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LegalEntityCode",
                table: "tb_CustomersAccountDetail",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CreditDebitLimit",
                table: "tb_CustomersAccountDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CorporateBookingCode",
                table: "tb_CustomersAccountDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "ApplyDisplayTDS",
                table: "tb_CustomersAccountDetail",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "AllowPersonalBooking",
                table: "tb_CustomersAccountDetail",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "AllowPartialPassthrough",
                table: "tb_CustomersAccountDetail",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "key_account_manager",
                table: "tb_CustomerManageStaff",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "User_group",
                table: "tb_CustomerManageStaff",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Sales_spoc",
                table: "tb_CustomerManageStaff",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Emergency_contact",
                table: "tb_CustomerManageStaff",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
