using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Entity
{
    [Table("tb_CustomerBand")]
    public class CustomerBand
    {
        [Key]
        public int BandID { get; set; }

        public string? LegalEntityCode { get; set; } = null;

        public string? BandName { get; set; }

        public string? BandCode { get; set; }
        public string? BandLevel { get; set; }
        public string? DefaultProduct { get; set; }

        public DateTime? CreatedDate { get; set; } = DateTime.Today;

        public string? CreatedBy { get; set; } = "Admin";

        public DateTime? UpdatedDate { get; set; }

        public string? UpdatedBy { get; set; } = "Admin";

        public bool IsActive { get; set; } = true;
    }
}
