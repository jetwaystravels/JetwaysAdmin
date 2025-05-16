using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Entity
{
    public class CustomersEmployee
    {
        [Key]
        public int UserID { get; set; }
        public string? LegalEntityCode { get; set; }
        public string? EmployeeID { get; set; }
        public string? Title { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? BusinessEmail { get; set; }
        public string? MobileCountryCode { get; set; }
        public string? MobileNumber { get; set; }
        public string? Nationality { get; set; }
        public string? WorkLocation { get; set; }
        public string? FrequentflyerNumber { get; set; }
        public string? SystemIntegrationRefNo { get; set; }
        public bool? ApprovalRequiredForBooking { get; set; }
        public bool? ApprovalRequiredForDeviation { get; set; }
        public string? GDSProfileType { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? AppStatus { get; set; } = 0;
    }
}
