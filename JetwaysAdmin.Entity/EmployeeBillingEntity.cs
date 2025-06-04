using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Entity
{
    public class EmployeeBillingEntity
    {
        [Key]
        public int OrganizationId { get; set; }
        public int UserID { get; set; }
        public string? LegalEntityCode { get; set; }
        public string? BillingEntityCode { get; set; }
        public string? Designation { get; set; }
        public string? Department { get; set; }
        public string? Band { get; set; }
        public string? WorkLocation { get; set; }
        public string? ReportingManager { get; set; }
        public string? EmploymentType { get; set; }
        public string? CostCenter { get; set; }
        public string? UserRole { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? SystemIntegrationRef { get; set; }
        public string CreatedBy { get; set; } = "Admin";
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public string ModifyBy { get; set; } = "Admin";
        public DateTime? ModifyDate { get; set; } = DateTime.Now;
        public int AppStatus { get; set; } = 0;

    }
}
