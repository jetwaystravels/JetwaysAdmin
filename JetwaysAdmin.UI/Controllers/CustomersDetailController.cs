using JetwaysAdmin.Entity;
using JetwaysAdmin.UI.ApplicationUrl;
using JetwaysAdmin.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JetwaysAdmin.UI.Controllers
{
    public class CustomersDetailController : Controller
    {
        public async Task<IActionResult> ShowCustomersDetail()
        {
            List<LegalEntity> legalEntities = new List<LegalEntity>();

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(AppUrlConstant.GetLegalEntity);
                //if (response.IsSuccessStatusCode)
                //{
                //    var result = await response.Content.ReadAsStringAsync();
                //    var responseData = JsonConvert.DeserializeObject<LegalEntityResponse>(result);
                //    legalEntities = responseData?.Data ?? new List<LegalEntity>();
                //}
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<LegalEntityResponse>(result);
                    legalEntities = responseData?.Data
                        ?.Where(le => string.IsNullOrEmpty(le.ParentLegalEntityCode))
                        .ToList() ?? new List<LegalEntity>();
                }
            }

            return View(legalEntities);
        }


    }
}
