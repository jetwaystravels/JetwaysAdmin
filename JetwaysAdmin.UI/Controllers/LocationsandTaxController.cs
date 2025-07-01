using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Migrations;
using JetwaysAdmin.UI.ApplicationUrl;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace JetwaysAdmin.UI.Controllers
{
    public class LocationsandTaxController : Controller
    {
        public IActionResult ShowLocationsandTax(string LegalEntityCode, string LegalEntityName)
        {
            ViewBag.LegalEntityCode = LegalEntityCode;
            ViewBag.LegalEntityName = LegalEntityName;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTaxLocation(LocationsandTax locationsandaax)
        {
            using (HttpClient client = new HttpClient())
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(locationsandaax);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsJsonAsync(AppUrlConstant.AddLoactionTax, locationsandaax);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    TempData["AddTax"] = "Tax & Location Add Successfully";
                }
                ViewBag.ErrorMessage = "Data not  insert";
                return RedirectToAction("ShowLocationsandTax");
            }
        }
    }
}
