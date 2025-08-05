using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Entity
{
    [Table("tb_CustomerDealCodes")]
    public class CustomerDealCodes
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DealCodeID { get; set; }
        public string LegalEntityCode { get; set; }
        public string SupplierId { get; set; }
        public string DealCodeName { get; set; }
        public string IATAGroup { get; set; }
        public string TravelType { get; set; }
        public string AssociatedFareTypes { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string DealPricingCode { get; set; }
        public string TourCode { get; set; }
        public string DealCodeType { get; set; }
        public string ClassOfSeats { get; set; }
        public bool GstMandatory { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string BookingType { get; set; }
        public string Status { get; set; }
    }
}
