using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JetwaysAdmin.Repositories.Migrations
{
    public partial class InitialCreatemy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_CustomersEmployee",
                table: "tb_CustomersEmployee");

            migrationBuilder.DropColumn(
                name: "CabinClass",
                table: "tb_CustomerDealCodes");

            migrationBuilder.DropColumn(
                name: "SelectedCredential",
                table: "tb_CustomerDealCodes");

            migrationBuilder.RenameColumn(
                name: "SupplierCode",
                table: "tb_CustomerDealCodes",
                newName: "AssociatedFareTypes");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "tb_CustomersEmployee",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Bands",
                table: "tb_CustomersEmployee",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "tb_CustomersEmployee",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "tb_CustomersEmployee",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Designation",
                table: "tb_CustomersEmployee",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LegalEntityName",
                table: "tb_CustomersEmployee",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReportingManager",
                table: "tb_CustomersEmployee",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UINRegistered",
                table: "tb_CustomerLocationTaxDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GSTRegistered",
                table: "tb_CustomerLocationTaxDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "tb_CustomerDealCodes",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "tb_CustomerDealCodes",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<bool>(
                name: "GstMandatory",
                table: "tb_CustomerDealCodes",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiryDate",
                table: "tb_CustomerDealCodes",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "tb_CustomerDealCodes",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "DealCodeID",
                table: "tb_CustomerDealCodes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "IATAGroup",
                table: "tb_CustomerDealCodes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                table: "tb_CustomerDealCodes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AssignIATAGroup",
                table: "CustomerDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FinancialPersonalCode",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FinancialType",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GSTLocationID",
                table: "Admin_tb_LegalEntity",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_CustomerDealCodes",
                table: "tb_CustomerDealCodes",
                column: "DealCodeID");

            migrationBuilder.CreateTable(
                name: "CustomersEmployeedto",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LegalEntityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileCountryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    SystemIntegrationRefNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovalRequiredForBooking = table.Column<bool>(type: "bit", nullable: true),
                    ApprovalRequiredForDeviation = table.Column<bool>(type: "bit", nullable: true),
                    GDSProfileType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifyBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AppStatus = table.Column<int>(type: "int", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bands = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportingManager = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomersEmployeedto", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "tb_CustomerDepartment",
                columns: table => new
                {
                    DepartmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LegalEntityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_CustomerDepartment", x => x.DepartmentID);
                });

            migrationBuilder.CreateTable(
                name: "tb_CustomerDesignation",
                columns: table => new
                {
                    DesignationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LegalEntityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DesignationCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DesignationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_CustomerDesignation", x => x.DesignationID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomersEmployeedto");

            migrationBuilder.DropTable(
                name: "tb_CustomerDepartment");

            migrationBuilder.DropTable(
                name: "tb_CustomerDesignation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_CustomerDealCodes",
                table: "tb_CustomerDealCodes");

            migrationBuilder.DropColumn(
                name: "Bands",
                table: "tb_CustomersEmployee");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "tb_CustomersEmployee");

            migrationBuilder.DropColumn(
                name: "Department",
                table: "tb_CustomersEmployee");

            migrationBuilder.DropColumn(
                name: "Designation",
                table: "tb_CustomersEmployee");

            migrationBuilder.DropColumn(
                name: "LegalEntityName",
                table: "tb_CustomersEmployee");

            migrationBuilder.DropColumn(
                name: "ReportingManager",
                table: "tb_CustomersEmployee");

            migrationBuilder.DropColumn(
                name: "IATAGroup",
                table: "tb_CustomerDealCodes");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "tb_CustomerDealCodes");

            migrationBuilder.DropColumn(
                name: "AssignIATAGroup",
                table: "CustomerDetails");

            migrationBuilder.DropColumn(
                name: "FinancialPersonalCode",
                table: "Admin_tb_LegalEntity");

            migrationBuilder.DropColumn(
                name: "FinancialType",
                table: "Admin_tb_LegalEntity");

            migrationBuilder.DropColumn(
                name: "GSTLocationID",
                table: "Admin_tb_LegalEntity");

            migrationBuilder.RenameColumn(
                name: "AssociatedFareTypes",
                table: "tb_CustomerDealCodes",
                newName: "SupplierCode");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "tb_CustomersEmployee",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<bool>(
                name: "UINRegistered",
                table: "tb_CustomerLocationTaxDetails",
                type: "bit",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "GSTRegistered",
                table: "tb_CustomerLocationTaxDetails",
                type: "bit",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "tb_CustomerDealCodes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "tb_CustomerDealCodes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "GstMandatory",
                table: "tb_CustomerDealCodes",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiryDate",
                table: "tb_CustomerDealCodes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "tb_CustomerDealCodes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DealCodeID",
                table: "tb_CustomerDealCodes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "CabinClass",
                table: "tb_CustomerDealCodes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SelectedCredential",
                table: "tb_CustomerDealCodes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_CustomersEmployee",
                table: "tb_CustomersEmployee",
                column: "UserID");
        }
    }
}
