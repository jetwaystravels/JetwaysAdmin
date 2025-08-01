using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Entity
{
    [Table("tb_SuppliersDetail")]
    public class AddSupplier
    {
        [Key]
        public int SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public string? SupplierCode { get; set; }
        public string? SupplierType { get; set; }
        public string? CarrierType { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? PinCode { get; set; }
        public string? SupplierEmails { get; set; }
        public byte[]? Logo { get; set; }
        public bool? SendAmendmentNotifications { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ModifyDate { get; set; } = DateTime.Now;
        public bool? AppStatus { get; set; } = false;
        //public string? Password { get; set; } = null;
        //public string? Domain { get; set; } = null;
        //public string? Username { get; set; } = null;
        public int? FlightCode { get; set; } = null;
        public string? Currency { get; set; } = null;
        public string? PhoneNumber { get; set; } = null;
        [NotMapped]  // <-- This ensures EF does not map this property
        public bool? IsActive { get; set; }


    }

    public class SupplierDto
    {
        public int SupplierID { get; set; }
        public string? SupplierName { get; set; }
        public string? SupplierCode { get; set; }
        public string? SupplierType { get; set; }
        public string? CarrierType { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? PinCode { get; set; }
        public string? SupplierEmails { get; set; }
        public byte[]? Logo { get; set; }

        // Match the SQL alias exactly  
        public bool? IsActive { get; set; }
        public bool? AppStatus { get; set; }



    }

}
