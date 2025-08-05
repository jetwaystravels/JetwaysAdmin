using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using JetwaysAdmin.Repositories.Migrations;
using JetwaysAdmin.UI.ApplicationUrl;
using JetwaysAdmin.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Xml.Linq;

namespace JetwaysAdmin.UI.Controllers
{
    public class LocationsandTaxController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> ShowLocationsandTax(int IdLegal, string LegalEntityCode, string LegalEntityName)
        {
            ViewBag.LegalEntityCode = LegalEntityCode;
            ViewBag.LegalEntityName = LegalEntityName;
            ViewBag.Id = IdLegal;
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
        
        public async Task<IActionResult> UpdateLocationTax(int LocationID, int Id, string LegalEntityCode, string LegalEntityName)
        {
           
            LocationsandTax locationtax = null;
            using (HttpClient client = new HttpClient())
            {
                string url = $"{AppUrlConstant.GetLoactionTaxID}/{LocationID}";
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    locationtax = JsonConvert.DeserializeObject<LocationsandTax>(result);
                }
            }
            ViewBag.LegalEntityCode = LegalEntityCode;
            ViewBag.LegalEntityName = LegalEntityName;
            ViewBag.Id = Id;
            return View(locationtax);
        }

        [HttpPost]
        public async Task<IActionResult> EditLocationTax(LocationsandTax locationsandtax, int IdParent, string LegalEntityCode, string LegalEntityName)
        {
            using (HttpClient client = new HttpClient())
            {

                string Data = JsonConvert.SerializeObject(locationsandtax);
                StringContent content = new StringContent(Data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync(AppUrlConstant.EditLoactionTaxID + "/" + locationsandtax.LocationID, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["updateLocation_message"] = "Location Tax Update";
                    return RedirectToAction("UpdateLocationTax", new
                    {
                        LocationID = locationsandtax.LocationID,
                        Id= IdParent,
                        LegalEntityCode= LegalEntityCode,
                        LegalEntityName= LegalEntityName
                    });
                }
            }
            ViewBag.LegalEntityCode = LegalEntityCode;
            ViewBag.LegalEntityName = LegalEntityName;
            ViewBag.Id = IdParent;
            return View();
        }
    }
}
