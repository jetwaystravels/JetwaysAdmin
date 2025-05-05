using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JetwaysAdmin.Repositories.Migrations
{
    public partial class leg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ManageAccountBalance",
                table: "Admin_tb_LegalEntity");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Admin_tb_LegalEntity",
                newName: "Createdby");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "tb_Menu",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "IsActive",
                table: "tb_Menu",
                type: "int",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "Action",
                table: "tb_Menu",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<decimal>(
                name: "PresentBalance",
                table: "CustomerDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "PassGSTDetailsAirline",
                table: "Admin_tb_LegalEntity",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "ModifyBy",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "GSTApplicableManagementFee",
                table: "Admin_tb_LegalEntity",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerBaseCurrency",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Admin_tb_LegalEntity",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Createdby",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "AppStatus",
                table: "Admin_tb_LegalEntity",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "AddressLine2",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "AddressLine1",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "AccountDeactivationDate",
                table: "Admin_tb_LegalEntity",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountType",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "AcountActivationDate",
                table: "Admin_tb_LegalEntity",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BussinesEmail",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "CreateNewGDSProfile",
                table: "Admin_tb_LegalEntity",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerBaseCountry",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MobileNumber",
                table: "Admin_tb_LegalEntity",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TravelDeskEmail",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "UpdateExistingCustomerProfile",
                table: "Admin_tb_LegalEntity",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Tb_Menuhead",
                columns: table => new
                {
                    MenuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_Menuhead", x => x.MenuId);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menu_Tb_Menuhead_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Tb_Menuhead",
                        principalColumn: "MenuId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Menu_ParentId",
                table: "Menu",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "Tb_Menuhead");

            migrationBuilder.DropColumn(
                name: "PresentBalance",
                table: "CustomerDetails");

            migrationBuilder.DropColumn(
                name: "AccountDeactivationDate",
                table: "Admin_tb_LegalEntity");

            migrationBuilder.DropColumn(
                name: "AccountType",
                table: "Admin_tb_LegalEntity");

            migrationBuilder.DropColumn(
                name: "AcountActivationDate",
                table: "Admin_tb_LegalEntity");

            migrationBuilder.DropColumn(
                name: "BussinesEmail",
                table: "Admin_tb_LegalEntity");

            migrationBuilder.DropColumn(
                name: "CreateNewGDSProfile",
                table: "Admin_tb_LegalEntity");

            migrationBuilder.DropColumn(
                name: "CustomerBaseCountry",
                table: "Admin_tb_LegalEntity");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Admin_tb_LegalEntity");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Admin_tb_LegalEntity");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Admin_tb_LegalEntity");

            migrationBuilder.DropColumn(
                name: "MobileNumber",
                table: "Admin_tb_LegalEntity");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Admin_tb_LegalEntity");

            migrationBuilder.DropColumn(
                name: "TravelDeskEmail",
                table: "Admin_tb_LegalEntity");

            migrationBuilder.DropColumn(
                name: "UpdateExistingCustomerProfile",
                table: "Admin_tb_LegalEntity");

            migrationBuilder.RenameColumn(
                name: "Createdby",
                table: "Admin_tb_LegalEntity",
                newName: "CreatedBy");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "tb_Menu",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "tb_Menu",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Action",
                table: "tb_Menu",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

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
                name: "ModifyBy",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

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
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

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
                name: "CustomerBaseCurrency",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Admin_tb_LegalEntity",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "AppStatus",
                table: "Admin_tb_LegalEntity",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AddressLine2",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "AddressLine1",
                table: "Admin_tb_LegalEntity",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<bool>(
                name: "ManageAccountBalance",
                table: "Admin_tb_LegalEntity",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
