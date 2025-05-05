using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace JetwaysAdmin.Entity
{
    public class CustomerAccountBalance
    {
        public int Id { get; set; }
        public bool? ManageAccountBalance { get; set; }
        public string AccountBalanceEntity { get; set; }
        public string BalanceType { get; set; }
        public int? OriginalLimit { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int? PresentBalance { get; set; }
        public int? AddAmmount { get; set; }
        public string Createdby { get; set; } = "Admin";
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public string ModifyBy { get; set; } = "Admin2";
        public DateTime? ModifyDate { get; set; } = DateTime.Now;
        public int? AppStatus { get; set; } = 1;

        //public string BillingEntityName { get; set; }
        //public decimal? CreditOrDeposit { get; set; }
        //public decimal? Debit { get; set; }

        //// Computed column, set to read-only
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //public decimal? TotalBalance { get; set; }
    }

    public class AccountBalanceTotal
    {
        public CustomerAccountBalance Balance { get; set; }
        public string BillingEntityName { get; set; }
        public decimal? CreditOrDeposit { get; set; }
        public decimal? Debit { get; set; }
        public decimal? TotalBalance => (CreditOrDeposit ?? 0) - (Debit ?? 0);
    }


}
