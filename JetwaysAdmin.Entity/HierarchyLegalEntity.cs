using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Entity
{
    public class HierarchyLegalEntity
    {
        public string LegalEntityCode { get; set; }
        public string? ParentLegalEntityCode { get; set; }
        public int? Level { get; set; }

        public int Id { get; set; }

        public string LegalEntityName {get; set; }

    }
}
