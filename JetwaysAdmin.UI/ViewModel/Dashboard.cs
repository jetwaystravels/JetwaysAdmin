using System.ComponentModel.DataAnnotations;

namespace JetwaysAdmin.UI.ViewModel
{
    public class Dashboard
    {
        [Key]
        public int Id { get; set; }
        public int CustomerCount { get; set; }
        public int SupplierCount { get; set; }
        public int IATAGroupsCount { get; set; }
        public int IATACommissionsCount { get; set; }
        public int MiniFareRulesCount { get; set; }
        public int ReportingParametersCount { get; set; }
        public int CabsCount { get; set; }
        public int BannersCount { get; set; }
    }
}
