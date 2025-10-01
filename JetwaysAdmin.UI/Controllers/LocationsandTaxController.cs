using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using JetwaysAdmin.Repositories.Migrations;
using JetwaysAdmin.UI.ApplicationUrl;
using JetwaysAdmin.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Extensions;
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
            List<State> state = new List<State>();
            var countryList = new List<Country>();
            using (HttpClient client = new HttpClient())
            {
                string requestUrl = $"{AppUrlConstant.GetLoactionTax}?LegalEntityCode={LegalEntityCode}";
                var userresponse = await client.GetAsync(requestUrl);
                if (userresponse.IsSuccessStatusCode)
                {
                    var result = await userresponse.Content.ReadAsStringAsync();
                    locationsandtax = JsonConvert.DeserializeObject<List<LocationsandTax>>(result);
                }
                string stateurl = $"{AppUrlConstant.GetSate}";
                var stateresponse = await client.GetAsync(stateurl);
                if (stateresponse.IsSuccessStatusCode)
                {
                    var stateresult = await stateresponse.Content.ReadAsStringAsync();
                    state = JsonConvert.DeserializeObject<List<State>>(stateresult);
                }
                var countryResponse = await client.GetAsync(AppUrlConstant.GetCountry);
                if (countryResponse.IsSuccessStatusCode)
                {
                    var result = await countryResponse.Content.ReadAsStringAsync();
                    countryList = JsonConvert.DeserializeObject<List<Country>>(result);
                }
            }
            var locationtax = new MenuHeaddata
            {
                LocationandTax = locationsandtax,
                Statedata=state

            };
            ViewBag.CountryList = countryList;
            return View(locationtax);
        }
        [HttpGet]
        public async Task<IActionResult> LoadStates(int CountryId)
        {
            List<State> states = new();

            using (HttpClient client = new HttpClient())
            {
                string url = $"{AppUrlConstant.GetSate}/{CountryId}";
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    states = JsonConvert.DeserializeObject<List<State>>(json);
                }
            }

            return Json(states);
        }
        [HttpGet]
        public async Task<IActionResult> LoadCities(int stateId)
        {
            List<City> cities = new();

            using (HttpClient client = new HttpClient())
            {
                string url = $"{AppUrlConstant.GetCity}/{stateId}";
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    cities = JsonConvert.DeserializeObject<List<City>>(json);
                }
            }
            return Json(cities);
        }


        [HttpPost]
        public async Task<IActionResult> AddTaxLocation(LocationsandTax locationsandaax,int IdLegal, string LegalEntityCode, string LegalEntityName, string GSTValue, string UINValue)
        {
            ViewBag.LegalEntityCode = LegalEntityCode;
            ViewBag.LegalEntityName = LegalEntityName;
            ViewBag.Id = IdLegal;
            locationsandaax.GSTRegistered = GSTValue == "G" ? "G" : null;
            locationsandaax.UINRegistered = UINValue == "U" ? "U" : null;
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
                        IdLegal = IdLegal
                    });
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to insert Tax & Location. Please try again.";
                    return RedirectToAction("AddLocationTax", new
                    {
                        LegalEntityName = LegalEntityName,
                        LegalEntityCode = LegalEntityCode,
                        IdLegal = IdLegal
                    });
                }
            }

        }
        
        public async Task<IActionResult> UpdateLocationTax(int LocationID, int IdLegal, string LegalEntityCode, string LegalEntityName)
        {
           
            LocationsandTax locationtax = null;
            var countryList = new List<Country>();
            using (HttpClient client = new HttpClient())
            {
                string url = $"{AppUrlConstant.GetLoactionTaxID}/{LocationID}";
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    locationtax = JsonConvert.DeserializeObject<LocationsandTax>(result);
                }
                var countryResponse = await client.GetAsync(AppUrlConstant.GetCountry);
                if (countryResponse.IsSuccessStatusCode)
                {
                    var result = await countryResponse.Content.ReadAsStringAsync();
                    countryList = JsonConvert.DeserializeObject<List<Country>>(result);
                }
            }
            ViewBag.LegalEntityCode = LegalEntityCode;
            ViewBag.LegalEntityName = LegalEntityName;
            ViewBag.Id = IdLegal;
            ViewBag.CountryList = countryList;
            return View(locationtax);
        }

        [HttpPost]
        public async Task<IActionResult> EditLocationTax(LocationsandTax locationsandtax, int IdLegal, string LegalEntityCode, string LegalEntityName)
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
                        IdLegal = IdLegal,
                        LegalEntityCode= LegalEntityCode,
                        LegalEntityName= LegalEntityName
                    });
                }
            }
            ViewBag.LegalEntityCode = LegalEntityCode;
            ViewBag.LegalEntityName = LegalEntityName;
            ViewBag.Id = IdLegal;
            return View();
        }
    }
}
