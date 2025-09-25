using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Entity
{
    public class CustomerDesignation
    {
        [Key]
        public int DesignationID { get; set; }
        public string? LegalEntityCode { get; set; }
        public string? DepartmentCode { get; set; }
        public string? DesignationCode { get; set; }
        public string? DesignationName { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Today;
        public string? CreatedBy { get; set; } = "Admin";
        public bool IsActive { get; set; } = true;
    }
}
