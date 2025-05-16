using JetwaysAdmin.Entity;

namespace JetwaysAdmin.UI.ViewModel
{
    public class HierarchyLegalEntityView
    {

        public int Id { get; set; }

        public string LegalEntityCode { get; set; }
        public string LegalEntityName { get; set; }
        public string ParentLegalEntityCode { get; set; }
        public int Level { get; set; }
        public string Status { get; set; }
        public List<HierarchyLegalEntityView> SubEntities { get; set; } = new();
       
  
        //public string Status { get; set; } // "Active" or "Deactivated"
        //public List<LegalEntity> SubEntities { get; set; } = new();
    }
}
