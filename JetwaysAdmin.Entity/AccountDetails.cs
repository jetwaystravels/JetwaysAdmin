using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Entity
{
    //Table Name tb_CustomersAccountDetail
    public class AccountDetails
    {

        [Key]
        public int AccountID { get; set; }

        public string? LegalEntityCode { get; set; } = "EEVB#56";

        public string IATAGroup { get; set; }

        public string CorporateBookingCode { get; set; }

        public bool AllowPersonalBooking { get; set; }

        public string PersonalBookingCode { get; set; }

        public string PANName { get; set; }

        public string PANNumber { get; set; }

        public bool ApplyDisplayTDS { get; set; }

        public string TDSRate { get; set; }

        public string PaymentMode { get; set; }

        public bool AllowPartialPassthrough { get; set; }

        public string CreditDebitLimit { get; set; }

    }
}
