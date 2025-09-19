using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Entity
{
    public class CustomersEmployeedto
    {
        [Key]
        public int UserID { get; set; }

        public string? LegalEntityCode { get; set; }
       
        public string EmployeeID { get; set; } = string.Empty;
        public string? Title { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? BusinessEmail { get; set; }
        public string? MobileCountryCode { get; set; }
        public string? MobileNumber { get; set; }
        public string? Nationality { get; set; }
        public string? WorkLocation { get; set; }

        public string? UserType { get; set; }
        public byte[]? Logo { get; set; }
        public string? SystemIntegrationRefNo { get; set; }
        public bool? ApprovalRequiredForBooking { get; set; } = true;
        public bool? ApprovalRequiredForDeviation { get; set; } = true;
        public string? GDSProfileType { get; set; }
        public string? CreatedBy { get; set; } = "Admin";
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public string? ModifyBy { get; set; } = "Admin";
        public DateTime? ModifyDate { get; set; } = DateTime.Now;
        public int? AppStatus { get; set; } = 0;
        public DateTime? DateOfBirth { get; set; }
        public string? Department { get; set; }
        public string? Designation { get; set; }
        public string? Bands { get; set; }
        public string? ReportingManager { get; set; }
    }
}
