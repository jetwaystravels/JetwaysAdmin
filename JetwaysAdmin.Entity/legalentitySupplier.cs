using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Entity
{
    public class legalentitySupplier
    {
        [Key]
        public int SupplierID { get; set; }
        public int AppStatus { get; set; }
        public string SupplierName { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierType { get; set; }
        public string CarrierType { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PinCode { get; set; }
        public string SupplierEmails { get; set; }
        public byte[] Logo { get; set; }
    }

    public class LegalEntitySupplierStatus
    {
        public int ID { get; set; }
        public string LegalEntityCode { get; set; }
        public int SupplierID { get; set; }
        public bool IsActive { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
