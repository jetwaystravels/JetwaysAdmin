using JetwaysAdmin.Entity;
using Newtonsoft.Json;

namespace JetwaysAdmin.UI.ViewModel
{
    public class LegalEntityResponse
    {
        public int TotalCount { get; set; }
        public List<LegalEntity> Data { get; set; }
    }


    public class UpdateLegalEntity
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

}
