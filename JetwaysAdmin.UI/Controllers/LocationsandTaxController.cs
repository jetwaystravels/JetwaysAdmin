using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using JetwaysAdmin.Repositories.Migrations;
using JetwaysAdmin.UI.ApplicationUrl;
using JetwaysAdmin.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace JetwaysAdmin.UI.Controllers
{
    public class LocationsandTaxController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> ShowLocationsandTax(int Id, string LegalEntityCode, string LegalEntityName)
        {
            ViewBag.LegalEntityCode = LegalEntityCode;
            ViewBag.LegalEntityName = LegalEntityName;
            ViewBag.Id = Id;
            List<LocationsandTax> locationsandtax = new List<LocationsandTax>();
            using (HttpClient client = new HttpClient())
            {
                string requestUrl = $"{AppUrlConstant.GetLoactionTax}?LegalEntityCode={LegalEntityCode}";
                var userresponse = await client.GetAsync(requestUrl);
                if (userresponse.IsSuccessStatusCode)
                {
                    var result = await userresponse.Content.ReadAsStringAsync();
                    locationsandtax = JsonConvert.DeserializeObject<List<LocationsandTax>>(result);
                }
            }
            var locationtax = new MenuHeaddata
            {
                LocationandTax = locationsandtax
            };
            return View(locationtax);
        }

        [HttpPost]
        public async Task<IActionResult> AddTaxLocation(LocationsandTax locationsandaax,int Id, string LegalEntityCode, string LegalEntityName)
        {

            using (HttpClient client = new HttpClient())
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(locationsandaax);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsJsonAsync(AppUrlConstant.AddLoactionTax, locationsandaax);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    TempData["AddTax"] = "Tax & Location added successfully";
                    return RedirectToAction("ShowLocationsandTax", new
                    {
                        LegalEntityName = LegalEntityName,
                        LegalEntityCode = LegalEntityCode,
                        Id = Id
                    });
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to insert Tax & Location. Please try again.";
                    return RedirectToAction("AddLocationTax", new
                    {
                        LegalEntityName = LegalEntityName,
                        LegalEntityCode = LegalEntityCode,
                        Id = Id
                    });
                }
            }

        }
    }
}
