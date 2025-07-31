using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JetwaysAdmin.Repositories.Migrations
{
    public partial class legalEntitySuppliersS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_DealCode",
                table: "tb_DealCode");

            migrationBuilder.DropColumn(
                name: "CredentialsType",
                table: "tb_SuppliersCredential");

            migrationBuilder.DropColumn(
                name: "SupplierCode",
                table: "tb_SuppliersCredential");

            migrationBuilder.DropColumn(
                name: "SupplierCode",
                table: "tb_DealCode");

            migrationBuilder.RenameTable(
                name: "tb_DealCode",
                newName: "tb_SuppliersDealCode");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "tb_SuppliersDealCode",
                newName: "StartDate");

            migrationBuilder.AddColumn<int>(
                name: "IATAGroup",
                table: "tb_SuppliersCredential",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                table: "tb_SuppliersCredential",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SelectedCredentialId",
                table: "tb_SuppliersDealCode",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                table: "tb_SuppliersDealCode",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_SuppliersDealCode",
                table: "tb_SuppliersDealCode",
                column: "DealCodeId");

            migrationBuilder.CreateTable(
                name: "_legalentitySupplier",
                columns: table => new
                {
                    SupplierID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppStatus = table.Column<int>(type: "int", nullable: false),
                    SupplierName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarrierType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PinCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierEmails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Logo = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__legalentitySupplier", x => x.SupplierID);
                });

            migrationBuilder.CreateTable(
                name: "LegalEntitySuppliersStatus",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LegalEntityCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierID = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalEntitySuppliersStatus", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_legalentitySupplier");

            migrationBuilder.DropTable(
                name: "LegalEntitySuppliersStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_SuppliersDealCode",
                table: "tb_SuppliersDealCode");

            migrationBuilder.DropColumn(
                name: "IATAGroup",
                table: "tb_SuppliersCredential");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "tb_SuppliersCredential");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "tb_SuppliersDealCode");

            migrationBuilder.RenameTable(
                name: "tb_SuppliersDealCode",
                newName: "tb_DealCode");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "tb_DealCode",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<string>(
                name: "CredentialsType",
                table: "tb_SuppliersCredential",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SupplierCode",
                table: "tb_SuppliersCredential",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SelectedCredentialId",
                table: "tb_DealCode",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SupplierCode",
                table: "tb_DealCode",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_DealCode",
                table: "tb_DealCode",
                column: "DealCodeId");
        }
    }
}
