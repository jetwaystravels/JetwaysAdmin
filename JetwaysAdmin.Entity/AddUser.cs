using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Entity
{
    //tb_AddNewUser  tablename
    public class AddUser
    {
        [Key]
        public int UserId { get; set; }
        public string LegalEntityId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CountryCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Nationality { get; set; }
        public string WorkLocation { get; set; }
        public string BusinessEmail { get; set; }
        public string EmailId { get; set; }
        public string SystemIntegrationRef { get; set; }
        public bool ApprovalRequiredForBooking { get; set; }
        public bool ApprovalRequiredForDeviation { get; set; }
        public bool GDSProfileType { get; set; }
        public bool ExistingCustomer { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
