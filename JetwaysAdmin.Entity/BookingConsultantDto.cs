using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Entity
{
    public class BookingConsultantDto
    {
        public string LegalEntityCode { get; set; }
        public string ?BookingConsultantNames { get; set; }

        public string? KeyAccountManagerNames { get; set; }

        public string ?SalesSpocNames { get; set; }

        public string? Emergency_contact { get; set; }

        public string ?User_group { get; set; }


    }

}
