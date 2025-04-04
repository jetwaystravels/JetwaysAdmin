using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Entity
{
    //[Table("Admin_tb_Customers")]
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }

        [Required, StringLength(255)]
        public string LegalEntityName { get; set; }

        [Required, StringLength(50)]
        public string LegalEntityCode { get; set; }

        [StringLength(255)]
        public string? AssignIATAGroup { get; set; }

        [StringLength(50)]
        public string? CorporateAccountsCode { get; set; }

        [StringLength(100)]
        public string? FirstName { get; set; }

        [StringLength(100)]
        public string? LastName { get; set; }

        [StringLength(255), EmailAddress]
        public string? BusinessEmailID { get; set; }

        [StringLength(255)]
        public string? PasswordHash { get; set; }

        [StringLength(20)]
        public string? MobileNumber { get; set; }

        [StringLength(10)]
        public string? Gender { get; set; }

        [Required, StringLength(255)]
        public string RegisteredAddressLine1 { get; set; }

        [StringLength(255)]
        public string? RegisteredAddressLine2 { get; set; }

        [Required, StringLength(100)]
        public string Country { get; set; }

        [Required, StringLength(100)]
        public string State { get; set; }

        [Required, StringLength(100)]
        public string City { get; set; }

        [Required, StringLength(20)]
        public string PostalCode { get; set; }

        [Required, StringLength(50)]
        public string AccountType { get; set; }

        [StringLength(100)]
        public string? IntegrationRefNumber { get; set; }

        [StringLength(10)]
        public string? CustomerBaseCurrency { get; set; } = "INR";

        [Required, StringLength(100)]
        public string CustomerBaseCountry { get; set; }

        public DateTime? AccountActivationDate { get; set; }
        public DateTime? AccountDeactivationDate { get; set; }

        public bool GSTApplicableOnManagementFee { get; set; } = false;
        public bool PassGSTDetailsToAirline { get; set; } = false;

        [StringLength(50)]
        public string? GDSProfileType { get; set; }

        [StringLength(255)]
        public string? LogoPath { get; set; }

        [StringLength(50)]
        public string? Createdby { get; set; }
        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public int? AppStatus { get; set; }
    }
}
