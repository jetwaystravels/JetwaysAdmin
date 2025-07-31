using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Entity
{
    public  class LegalEntitySupplierDto
    {
        public string LegalEntityCode { get; set; }
        public int SupplierID { get; set; }
        public bool IsActive { get; set; }
    }
}
