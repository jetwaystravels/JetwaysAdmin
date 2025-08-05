using JetwaysAdmin.Entity;
using JetwaysAdmin.UI.ApplicationUrl;
using JetwaysAdmin.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace JetwaysAdmin.UI.Controllers
{
    public class AccountDetailsController : Controller
    {
        public async Task<IActionResult> ShowAccountDetails(int IdLegal, string LegalEntityCode, string LegalEntityName)
        {
            ViewBag.LegalEntityCode = LegalEntityCode;
            ViewBag.LegalEntityName = LegalEntityName;
            ViewBag.Id = IdLegal;
            List<IATAGroupView> iataGroups = new List<IATAGroupView>();
            using (HttpClient client = new HttpClient())
            {
                var iataResponse = await client.GetAsync(AppUrlConstant.GetIATAGroup);
                if (iataResponse.IsSuccessStatusCode)
                {
                    var result = await iataResponse.Content.ReadAsStringAsync();
                    iataGroups = JsonConvert.DeserializeObject<List<IATAGroupView>>(result);
                }
            }

            var viewModel = new MenuHeaddata
            {
                IATAGruopName = iataGroups
            };
            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> AddAccountDetails([FromForm] AccountDetails accountdetails)
        {
            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(accountdetails);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsJsonAsync(AppUrlConstant.AddAccountDetails, accountdetails);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                }
                ViewBag.ErrorMessage = "Data not  insert";
                return RedirectToAction("ShowAccountDetails");
            }
        }
    }
}
