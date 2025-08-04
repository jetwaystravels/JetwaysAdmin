using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Migrations;
using JetwaysAdmin.UI.ApplicationUrl;
using JetwaysAdmin.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;
using System.Xml.Linq;

namespace JetwaysAdmin.UI.Controllers
{
    public class OrganizationStructureController : Controller
    {
        public async Task<IActionResult> ShowOrganization(int IdLegal, string legalEntityName, string legalEntityCode)
        {
            List<LegalEntity> rootEntity = new List<LegalEntity>();

            using (HttpClient client = new HttpClient())
            {
                string apiLegalEntityUrl = $"{AppUrlConstant.GetLegalEntity}";
                HttpResponseMessage response = await client.GetAsync(apiLegalEntityUrl);

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        var responseObject = JsonConvert.DeserializeObject<LegalEntityResponse>(result);
                        rootEntity = responseObject?.Data;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error during deserialization: " + ex.Message);
                    }
                    var parent = rootEntity.FirstOrDefault(e => e.LegalEntityCode == legalEntityCode);
                    if (parent == null)
                        return View(new List<LegalEntity>());
                    var children = rootEntity
                        .Where(e => e.ParentLegalEntityCode == legalEntityCode)
                        .ToList();
                    var combined = new List<LegalEntity> { parent };
                    combined.AddRange(children);
                    ViewBag.LegalEntityCode = legalEntityCode;
                    ViewBag.LegalEntityName = legalEntityName;
                    ViewBag.Id = IdLegal;
                    return View(combined);
                }
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ShowOffice(int IdLegal, string LegalEntityCode, string LegalEntityName)
        {
            ViewBag.LegalEntityCode = LegalEntityCode;
            ViewBag.LegalEntityName = LegalEntityName;
            ViewBag.Id = IdLegal;
            var iataGroups = new List<IATAGroupView>();
            var countryList = new List<Country>();

            using (HttpClient client = new HttpClient())
            {
                var iataResponse = await client.GetAsync(AppUrlConstant.GetIATAGroup);
                if (iataResponse.IsSuccessStatusCode)
                {
                    var result = await iataResponse.Content.ReadAsStringAsync();
                    iataGroups = JsonConvert.DeserializeObject<List<IATAGroupView>>(result);
                }

                var countryResponse = await client.GetAsync(AppUrlConstant.GetCountry);
                if (countryResponse.IsSuccessStatusCode)
                {
                    var result = await countryResponse.Content.ReadAsStringAsync();
                    countryList = JsonConvert.DeserializeObject<List<Country>>(result);
                }
            }

            var viewModel = new MenuHeaddata
            {
                IATAGruopName = iataGroups,
            };
            ViewBag.CountryList = countryList;
            return PartialView("_OfficeModel", viewModel);
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
        public async Task<IActionResult> AddOffice([FromForm] LegalEntity legalEntity,int IdLegal, string ParentLegalEntityCode, string ParentLegalEntityName)
        {
            ViewBag.LegalEntityCode = ParentLegalEntityCode;
            ViewBag.LegalEntityName = ParentLegalEntityName;
            ViewBag.Id = IdLegal;
            using (HttpClient client = new HttpClient())
            {
                //var legalentityalldata = await client.GetAsync(AppUrlConstant.GetLegalEntity);
                //if (legalentityalldata.IsSuccessStatusCode)
                //{
                //    var existingJson = await legalentityalldata.Content.ReadAsStringAsync();
                //    var parsedResult = JsonConvert.DeserializeObject<LegalEntityResponse>(existingJson);
                //    var existingData = parsedResult?.Data ?? new List<LegalEntity>();
                //    bool isDuplicate = existingData.Any(e =>
                //        e.LegalEntityName.Equals(legalEntity.LegalEntityName, StringComparison.OrdinalIgnoreCase) ||
                //        e.LegalEntityCode.Equals(legalEntity.LegalEntityCode, StringComparison.OrdinalIgnoreCase) ||
                //        (!string.IsNullOrEmpty(e.CorporateAccountsCode) &&
                //         e.CorporateAccountsCode.Equals(legalEntity.CorporateAccountsCode, StringComparison.OrdinalIgnoreCase))
                //    );
                //    if (isDuplicate)
                //    {
                //        TempData["DuplicateError"] = "Duplicate entry found! Please enter unique Legal Entity Name, Code, or Corporate Account Code.";
                //        return RedirectToAction("ShowOrganization", new { LegalEntityCode = ParentLegalEntityCode, LegalEntityName = ParentLegalEntityName });
                //    }
                //}
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(legalEntity);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsJsonAsync(AppUrlConstant.AddLegalEntity, legalEntity);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    TempData["LegalAdd"] = "Office Add Successfully";
                }
                ViewBag.ErrorMessage = "Data not  insert";
                return RedirectToAction("ShowOrganization", new { LegalEntityCode = ParentLegalEntityCode, LegalEntityName = ParentLegalEntityName });
            }
        }

        public async Task<IActionResult> UpdateOffice(int Id, int IdLegal, string LegalEntityCode, string LegalEntityName)
        {
            List<IATAGroupView> iataGroups = new();
            List<Country> countryList = new();
            LegalEntity entity = null;
            using (HttpClient client = new HttpClient())
            {
                string url = $"{AppUrlConstant.GetLegalEntityID}/{Id}";
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    entity = JsonConvert.DeserializeObject<LegalEntity>(result);
                }
           
                var iataResponse = await client.GetAsync(AppUrlConstant.GetIATAGroup);
                if (iataResponse.IsSuccessStatusCode)
                {
                    var result = await iataResponse.Content.ReadAsStringAsync();
                    iataGroups = JsonConvert.DeserializeObject<List<IATAGroupView>>(result);
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
            var viewModel = new MenuHeaddata
            {
                LegalEntitydata = entity != null ? new List<LegalEntity> { entity } : new List<LegalEntity>(),
                IATAGruopName = iataGroups
            };
            return PartialView("_OfficeUpdate", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditOffice(LegalEntity legalEntity, int  IdLegal, string ParentLegalEntityCode, string ParentLegalEntityName)
        {
            int Id = legalEntity.Id;
            using (HttpClient client = new HttpClient())
            {
                ViewBag.LegalEntityCode = ParentLegalEntityCode;
                ViewBag.LegalEntityName = ParentLegalEntityName;
                ViewBag.Id = IdLegal;
                string Data = JsonConvert.SerializeObject(legalEntity);
                StringContent content = new StringContent(Data, Encoding.UTF8, "application/json");
                HttpResponseMessage response =client.PutAsync(AppUrlConstant.EditLegalEntityID + "/" + legalEntity.Id, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["officeupdate_message"] = "Office Update";
                    return RedirectToAction("ShowOrganization", new
                    {
                        IdLegal = IdLegal,
                        legalEntityCode = ParentLegalEntityCode,  
                        legalEntityName = ParentLegalEntityName
                    });
                }
            }
            return View();
        }

    }
}