using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Entity
{
    public class CustomerDealCodeModel
    {
        public string LegalEntityCode { get; set; }
        public string SupplierCode { get; set; }
        public string DealCodeName { get; set; }
        public int IATAGroup { get; set; }
        public string TravelType { get; set; }
        public List<string> AssociatedFareTypes { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string DealPricingCode { get; set; }
        public string TourCode { get; set; }
        public string DealCodeType { get; set; }
        public string ClassOfSeats { get; set; }
        public bool GstMandatory { get; set; }
        public string BookingType { get; set; }
    }
}
