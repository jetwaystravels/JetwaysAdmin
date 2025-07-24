using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using JetwaysAdmin.UI.ApplicationUrl;
using JetwaysAdmin.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;
using System.Text;
using System.Xml.Linq;

namespace JetwaysAdmin.UI.Controllers
{
    public class LegalEntityController : Controller
    {
       
        public async Task<IActionResult> ShowLegalEntities(string LegalEntityCode, string LegalEntityName)
        {
            ViewBag.LegalEntityCode = LegalEntityCode;
            ViewBag.LegalEntityName = LegalEntityName;
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
                IATAGruopName = iataGroups,
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddLegalEntity([FromForm] LegalEntity legalEntity, string LegalEntityCode, string LegalEntityName)
        {
            using (HttpClient client = new HttpClient())
            {
                var legalentityalldata = await client.GetAsync(AppUrlConstant.GetLegalEntity);
                if (legalentityalldata.IsSuccessStatusCode)
                {
                    var existingJson = await legalentityalldata.Content.ReadAsStringAsync();
                    var parsedResult = JsonConvert.DeserializeObject<LegalEntityResponse>(existingJson);
                    var existingData = parsedResult?.Data ?? new List<LegalEntity>();
                    bool isDuplicate = existingData.Any(e =>
                        e.LegalEntityName.Equals(legalEntity.LegalEntityName, StringComparison.OrdinalIgnoreCase) ||
                        e.LegalEntityCode.Equals(legalEntity.LegalEntityCode, StringComparison.OrdinalIgnoreCase) ||
                        (!string.IsNullOrEmpty(e.CorporateAccountsCode) &&
                         e.CorporateAccountsCode.Equals(legalEntity.CorporateAccountsCode, StringComparison.OrdinalIgnoreCase))
                    );
                    if (isDuplicate)
                    {
                        TempData["DuplicateError"] = "Duplicate entry found! Please enter unique Legal Entity Name, Code, or Corporate Account Code.";
                        return RedirectToAction("ShowLegalEntities", new { LegalEntityCode = LegalEntityCode, LegalEntityName = LegalEntityName });
                    }
                }
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(legalEntity);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsJsonAsync(AppUrlConstant.AddLegalEntity, legalEntity);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    TempData["LegalAdd"] = "Legal Entity Add Successfully";
                }
                ViewBag.ErrorMessage = "Data not  insert";
                return RedirectToAction("ShowLegalEntities", new { legalEntity = legalEntity , LegalEntityCode = LegalEntityCode });
            }
        }

        public async Task<IActionResult> UpdateLegalEntities(int Id, string Code, string Name)
        {
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
            }
            ViewBag.LegalEntityCode = Code;
            ViewBag.LegalEntityName = Name;
            ViewBag.Id = Id;
            return View(entity);
        }

        [HttpPost]
        public async Task<IActionResult> EditLegalEntities(LegalEntity legalEntity)
        {
            using (HttpClient client = new HttpClient())
            {

                string Data = JsonConvert.SerializeObject(legalEntity);
                StringContent content = new StringContent(Data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync(AppUrlConstant.EditLegalEntityID + "/" + legalEntity.Id, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["update_message"] = "Entities Update";
                    return RedirectToAction("UpdateLegalEntities", new
                    {
                        Id = legalEntity.Id,
                        Code = legalEntity.LegalEntityCode,
                        Name = legalEntity.LegalEntityName
                    });
                }
            }
            return View();
        }






    }
}
