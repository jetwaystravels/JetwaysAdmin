using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Entity
{
    //Table Name tb_IATAGroup
    public class IATAGroup
    {
        [Key]
        public int GroupID { get; set; }
        public string IATACode { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyByDate { get; set; }
        public bool IsActive { get; set; }
    }
}
