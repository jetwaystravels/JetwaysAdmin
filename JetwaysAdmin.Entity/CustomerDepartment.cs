using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Entity
{
    public class CustomerDepartment
    {
        [Key]
        public int DepartmentID { get; set; }
        public string? LegalEntityCode { get; set; }
        public string DepartmentCode { get; set; } = string.Empty;
        public string? DepartmentName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
