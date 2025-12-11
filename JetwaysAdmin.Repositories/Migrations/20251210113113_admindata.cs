using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JetwaysAdmin.Repositories.Migrations
{
    public partial class admindata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "tb_Booking",
                schema: "dbo",
                columns: table => new
                {
                    BookingID = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    FlightID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AirLineID = table.Column<int>(type: "int", nullable: true),
                    RecordLocator = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CurrencyCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BookedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Origin = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Destination = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DepartureDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ArrivalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "numeric(18,0)", nullable: true),
                    SpecialServicesTotal = table.Column<decimal>(type: "numeric(18,0)", nullable: true),
                    SpecialServicesTotal_Tax = table.Column<decimal>(type: "numeric(18,0)", nullable: true),
                    SeatTotalAmount = table.Column<decimal>(type: "numeric(18,0)", nullable: true),
                    SeatTotalAmount_Tax = table.Column<decimal>(type: "numeric(18,0)", nullable: true),
                    SeatAdjustment = table.Column<decimal>(type: "numeric(18,0)", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Createdby = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifyBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BookingDoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookingStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TicketNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PromoCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CancelStatus = table.Column<int>(type: "int", nullable: false),
                    CancelDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaidStatus = table.Column<int>(type: "int", nullable: true),
                    BookingType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BookingRelationID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TripType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Consultant = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Addons = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Booking", x => x.BookingID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_Booking",
                schema: "dbo");
        }
    }
}
