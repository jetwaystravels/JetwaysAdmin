using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JetwaysAdmin.Entity
{
    //[Table("Admin_tb_LegalEntity")]
    public class LegalEntity
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? LegalEntityName { get; set; }

        public string? LegalEntityCode { get; set; }

        public string? ParentLegalEntityCode { get; set; } = "EEEBP";

        public string? AssignIATAGroup { get; set; }

        public string? CorporateAccountsCode { get; set; }

        [MaxLength(50)]
        public string? FirstName { get; set; }

        [MaxLength(50)]
        public string? LastName { get; set; }

        [MaxLength(60)]
        public string? BussinesEmail { get; set; }

        [MaxLength(30)]
        public string? Password { get; set; }

        public string? MobileNumber { get; set; }

        [MaxLength(10)]
        public string? Gender { get; set; }

        [MaxLength(255)]
        public string? AddressLine1 { get; set; }

        [MaxLength(255)]
        public string? AddressLine2 { get; set; }

        [MaxLength(100)]
        public string? Country { get; set; }

        [MaxLength(100)]
        public string? State { get; set; }

        [MaxLength(100)]
        public string? City { get; set; }

        [MaxLength(20)]
        public string? PostalCode { get; set; }

        [MaxLength(40)]
        public string? AccountType { get; set; }

        [MaxLength(100)]
        public string? IntegrationRefNumber { get; set; }

        public bool? GSTApplicableManagementFee { get; set; }

        public bool? PassGSTDetailsAirline { get; set; }

        public bool? IsSEZ { get; set; } = true;

        [MaxLength(50)]
        public string? CustomerBaseCurrency { get; set; }

        [MaxLength(50)]
        public string? CustomerBaseCountry { get; set; }

        [MaxLength(50)]
        public string? TravelDeskEmail { get; set; }

        public DateTime? AcountActivationDate { get; set; }

        public DateTime? AccountDeactivationDate { get; set; }

        public bool? CreateNewGDSProfile { get; set; }

        public bool? UpdateExistingCustomerProfile { get; set; }

        // Uncomment when implementing logo support
        // public byte[] Logo { get; set; }

        [MaxLength(50)]
        public string Createdby { get; set; } = "Admin";

        public DateTime? CreatedDate { get; set; } = DateTime.Now;

        [MaxLength(50)]
        public string ModifyBy { get; set; } = "Admin2";

        public DateTime? ModifyDate { get; set; } = DateTime.Now;

        public int? AppStatus { get; set; } = 1;
    }
}
