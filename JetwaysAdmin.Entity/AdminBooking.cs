using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JetwaysAdmin.Entity
{
    [Table("tb_Booking", Schema = "dbo")]
    public class AdminBooking
    {
        // Table has both Id (identity) and BookingID (PK on BookingID)
        // We'll treat BookingID as the EF primary key (same as SQL).
        [Key]
        [MaxLength(100)]
        public string BookingID { get; set; } = null!;

        public int Id { get; set; }   // identity column, not the key in SQL

        [MaxLength(50)]
        public string? FlightID { get; set; }

        public int? AirLineID { get; set; }

        [MaxLength(50)]
        public string? RecordLocator { get; set; }

        [MaxLength(50)]
        public string? CurrencyCode { get; set; }

        public DateTime? BookedDate { get; set; }

        [MaxLength(50)]
        public string? Origin { get; set; }

        [MaxLength(50)]
        public string? Destination { get; set; }

        public DateTime? DepartureDate { get; set; }
        public DateTime? ArrivalDate { get; set; }

        public decimal? TotalAmount { get; set; }
        public decimal? SpecialServicesTotal { get; set; }
        public decimal? SpecialServicesTotal_Tax { get; set; }
        public decimal? SeatTotalAmount { get; set; }
        public decimal? SeatTotalAmount_Tax { get; set; }
        public decimal? SeatAdjustment { get; set; }

        public DateTime? ExpirationDate { get; set; }
        public DateTime? CreatedDate { get; set; }

        [MaxLength(50)]
        public string? Createdby { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [MaxLength(50)]
        public string? ModifyBy { get; set; }

        public string? BookingDoc { get; set; }

        [MaxLength(50)]
        public string? BookingStatus { get; set; }

        [MaxLength(50)]
        public string? TicketNumber { get; set; }

        [MaxLength(50)]
        public string? PromoCode { get; set; }

        public int CancelStatus { get; set; }
        public DateTime? CancelDate { get; set; }
        public int? PaidStatus { get; set; }

        [MaxLength(50)]
        public string? BookingType { get; set; }

        [MaxLength(50)]
        public string? BookingRelationID { get; set; }

        [MaxLength(50)]
        public string? TripType { get; set; }

        [MaxLength(50)]
        public string? CompanyName { get; set; }

        [MaxLength(50)]
        public string? Consultant { get; set; }

        [MaxLength(50)]
        public string? Mobile { get; set; }

        [MaxLength(50)]
        public string? Addons { get; set; }
    }
}
