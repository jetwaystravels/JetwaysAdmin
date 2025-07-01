using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Entity
{
    public class DealCode
    {
        [Key]
        public int DealCodeId { get; set; }
        public string? PCC { get; set; }
        public string? DealCodeName { get; set; }
        public string? SelectedCredentialId { get; set; }
        public string? TravelMode { get; set; }
        public string? DealPricingCode { get; set; }
        public string? TourCode { get; set; }
        public string? AssociatedFareTypes { get; set; }
        public string? CabinClass { get; set; }
        public string? DefaultValue { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? ClassOfSeats { get; set; }
        public string? SupplierCode { get; set; }
        public bool? AutoEnableDealCode { get; set; } = false;
        public bool? GSTMandatory { get; set; } = false;
        public bool? OverrideCustomerGST { get; set; } = false;
        public string? BookingType { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
