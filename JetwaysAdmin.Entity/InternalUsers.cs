using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Entity
{
    public class InternalUsers
    {
        [Key]
        public int UserID { get; set; }
        public string? LegalEntity { get; set; }
        public string? EmpId { get; set; }
        public string? Title { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? BusinessEmail { get; set; }
        public string? MobileNumber { get; set; }
        public string? Nationality { get; set; }
        public string? WorkLocation { get; set; }
        public string? UserType { get; set; }
        public string? SystemIntegrationRef { get; set; }
        public bool? ApprovalDeviationBooking { get; set; } = true;
        public bool? ApprovalRequiredBooking { get; set; } = true;
        public byte[]? Logo { get; set; }
        public string? GDSProfileType { get; set; }
        public string? Createdby { get; set; } = "Admin";    
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string ModifyBy { get; set; } = "Admin";
        public DateTime? ModifyDate { get; set; }= DateTime.Now;
        public bool? AppStatus { get; set; } = true;

      
    }
}
