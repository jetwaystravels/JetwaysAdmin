using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Entity
{
    public class EmployeeFrequentFlyer
    {
        [Key]
        public int FrequentFlyerID { get; set; }
        public int? EmployeeID { get; set; }
        public int? AirlineID { get; set; }
        public string? FrequentFlyerNumber { get; set; }
        public string? MembershipLevel { get; set; }
        public string LegalEntityCode { get; set; }
        public DateTime? EnrollmentDate { get; set; } = DateTime.Now;


    }
}
