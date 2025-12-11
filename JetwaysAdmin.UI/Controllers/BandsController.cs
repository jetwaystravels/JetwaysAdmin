using JetwaysAdmin.Entity;
using JetwaysAdmin.UI.ApplicationUrl;
using JetwaysAdmin.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace JetwaysAdmin.UI.Controllers
{
    public class BandsController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> ShowBands()
        {
            List<CustomerBand> customerBands = new List<CustomerBand>();

            using (HttpClient client = new HttpClient())
            {
                string requestUrl = AppUrlConstant.CustomerBand;
                var response = await client.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    customerBands = JsonConvert.DeserializeObject<List<CustomerBand>>(result);
                }
            }

            var viewModel = new MenuHeaddata
            {
                Customerbands = customerBands
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddBand(CustomerBand customerBand)
        {
            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(customerBand);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response =
                    await client.PostAsync(AppUrlConstant.AddCustomerBand, content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["BandAdd"] = "Band Added Successfully";
                    return RedirectToAction("ShowBands");
                }
            }

            TempData["BandAdd"] = "Failed to Add Band";
            return RedirectToAction("ShowBands");
        }
    }
}
