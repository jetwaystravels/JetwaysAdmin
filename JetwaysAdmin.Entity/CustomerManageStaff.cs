using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Entity
{
    public  class CustomerManageStaff
    {
        [Key]
        public int Id { get; set; }

        [Column("key_account_manager")]
        public string? KeyAccountManager { get; set; }

        [Column("Sales_spoc")]
        public string? SalesSpoc { get; set; }

        [Column("Booking_consultant")]
       
        public string? BookingConsultant { get; set; }

        [Column("Emergency_contact")]
        public string? EmergencyContact { get; set; }

        [Column("User_group")]
        public string? UserGroup { get; set; }
        [Required]
        public string LegalEntityCode { get; set; }
    }
}
