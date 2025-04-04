using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Entity
{
    //[Table("Admin_tb_LegalEntity")]
    public class LegalEntity
    {
        [Key]
       // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        
        public string LegalEntityName { get; set; }

        
        public string LegalEntityCode { get; set; }

        
        public string ParentLegalEntityCode { get; set; }

        
        public string AssignIATAGroup { get; set; }

        
        public string CorporateAccountsCode { get; set; }

        public bool ManageAccountBalance { get; set; }

        
        public string AddressLine1 { get; set; }

        
        public string AddressLine2 { get; set; }

        
        public string Country { get; set; }

        
        public string State { get; set; }

        
        public string City { get; set; }

       
        public string PostalCode { get; set; }

        
        public string IntegrationRefNumber { get; set; }

       
        public bool GSTApplicableManagementFee { get; set; }

       
        public bool PassGSTDetailsAirline { get; set; }

       
        public bool IsSEZ { get; set; }

        public string CustomerBaseCurrency { get; set; }

     
        //public byte[] Logo { get; set; }

        
        public string CreatedBy { get; set; }

       
        public DateTime CreatedDate { get; set; }

        public string ModifyBy { get; set; }

        public DateTime? ModifyDate { get; set; }

       
        public int AppStatus { get; set; }
    }
}
