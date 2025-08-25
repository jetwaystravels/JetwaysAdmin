using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using JetwaysAdmin.Repositories.Migrations;
using JetwaysAdmin.UI.ApplicationUrl;
using JetwaysAdmin.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Text;

namespace JetwaysAdmin.UI.Controllers
{
    public class FrequentFlyerMapController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> ShowFrequentFlyerMap(int? employeeId = null, int IdLegal = 0, string LegalEntityCode = "", string LegalEntityName = "")
        {
            List<CustomersEmployee> manageuser = new List<CustomersEmployee>();
            List<AddSupplier> supplier = new List<AddSupplier>();
            List<EmployeeFrequentFlyer> frequentflyerList = new List<EmployeeFrequentFlyer>();
            List<FrequentFlyerDisplay> flyerDisplayList = new List<FrequentFlyerDisplay>();
            using (HttpClient client = new HttpClient())
            {
                // var userresponse = await client.GetAsync(AppUrlConstant.GetCustomerEmployee);
                string requestUrl = $"{AppUrlConstant.GetCustomerEmployee}?LegalEntityCode={LegalEntityCode}";
                var userresponse = await client.GetAsync(requestUrl);
                if (userresponse.IsSuccessStatusCode)
                {
                    var result = await userresponse.Content.ReadAsStringAsync();
                    manageuser = JsonConvert.DeserializeObject<List<CustomersEmployee>>(result);
                }
                var getsupplier = await client.GetAsync(AppUrlConstant.GetSupplier);
                if (userresponse.IsSuccessStatusCode)
                {
                    var resultSupplier = await getsupplier.Content.ReadAsStringAsync();
                    supplier = JsonConvert.DeserializeObject<List<AddSupplier>>(resultSupplier);
                }

                var flyerResponse = await client.GetAsync(AppUrlConstant.GetFrequentFlyer);
                if (flyerResponse.IsSuccessStatusCode)
                {
                    var flyerResult = await flyerResponse.Content.ReadAsStringAsync();
                    frequentflyerList = JsonConvert.DeserializeObject<List<EmployeeFrequentFlyer>>(flyerResult);
                }
            }
            flyerDisplayList = (from f in frequentflyerList
                                join e in manageuser on f.EmployeeID equals e.UserID
                                join s in supplier on f.AirlineID equals s.SupplierId
                                select new FrequentFlyerDisplay
                                {
                                    EmployeeID = e.EmployeeID,
                                    UserID = e.UserID,
                                    EmployeeName = $"{e.FirstName} {e.LastName}",
                                    SupplierName = s.SupplierName,
                                    SupplierCode = s.SupplierCode,
                                    FrequentFlyerNumber = f.FrequentFlyerNumber,
                                    FrequentFlyerID =f.FrequentFlyerID
                                }).OrderBy(x => x.EmployeeID) // Ensure grouped display
                        .ToList();

            if (employeeId.HasValue)
                            {
                                flyerDisplayList = flyerDisplayList.Where(x => x.UserID == employeeId.Value).ToList();
                            }
                            else
                            {
                                flyerDisplayList = flyerDisplayList.ToList();
                            }

                            var viewModel = new MenuHeaddata
                            {
                                customersemployee = manageuser,
                                getsupplier = supplier,
                                flyerList = frequentflyerList,
                                FlyerDisplayList = flyerDisplayList
                            };

            ViewBag.LegalEntityCode = LegalEntityCode;
            ViewBag.LegalEntityName = LegalEntityName;
            ViewBag.Id = IdLegal;

            return View(viewModel);
        }



        [HttpPost]
        public async Task<IActionResult> AddFrequentFlyer([FromForm] EmployeeFrequentFlyer frequentFlyer, int IdLegal, string LegalEntityCode, string LegalEntityName)
        {

            List<EmployeeFrequentFlyer> frequentflyerList = new List<EmployeeFrequentFlyer>();

            using (HttpClient client = new HttpClient())
            {
                var userresponse = await client.GetAsync(AppUrlConstant.GetFrequentFlyer);
                if (userresponse.IsSuccessStatusCode)
                {
                    var result = await userresponse.Content.ReadAsStringAsync();
                    frequentflyerList = JsonConvert.DeserializeObject<List<EmployeeFrequentFlyer>>(result);
                }

                bool isDuplicate = frequentflyerList.Any(x =>
                    x.EmployeeID == frequentFlyer.EmployeeID &&
                    x.AirlineID == frequentFlyer.AirlineID);

                if (isDuplicate)
                {
                    ViewBag.ErrorMessage = "This Employee + Airline combination already exists.";
                    var viewResult = await ShowFrequentFlyerMap(frequentFlyer.EmployeeID, IdLegal, LegalEntityCode, LegalEntityName) as ViewResult;

                    //ViewBag.EmployeeId = "";
                    ViewBag.LegalEntityCode = LegalEntityCode;
                    ViewBag.LegalEntityName = LegalEntityName;
                    ViewBag.Id = IdLegal;

                    return View("ShowFrequentFlyerMap", viewResult.Model);
                }
                frequentFlyer.LegalEntityCode = LegalEntityCode;
                var json = JsonConvert.SerializeObject(frequentFlyer);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(AppUrlConstant.AddFrequentFlyer, content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("ShowFrequentFlyerMap", new { EmployeeID = frequentFlyer.EmployeeID, IdLegal = IdLegal, LegalEntityCode = LegalEntityCode, LegalEntityName = LegalEntityName });
                }

                ViewBag.ErrorMessage = "Data not inserted.";
                var errorViewResult = await ShowFrequentFlyerMap() as ViewResult;
                return View("ShowFrequentFlyerMap", errorViewResult.Model);
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetFrequentFlyerById(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(AppUrlConstant.GetFrequentFlyer);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var flyerList = JsonConvert.DeserializeObject<List<EmployeeFrequentFlyer>>(json);

                    var flyer = flyerList.FirstOrDefault(f => f.FrequentFlyerID == id);
                    if (flyer != null)
                        return Json(flyer);
                }
            }

            return NotFound();
        }


        [HttpPost]
        public async Task<IActionResult> UpdateFrequentFlyer([FromBody] EmployeeFrequentFlyer flyer)
        {
            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(flyer);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync(AppUrlConstant.UpdateFrequentFlyer + "/" + flyer.FrequentFlyerID, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["update_message"] = "Frequent Flyer updated successfully.";
                    return RedirectToAction("ShowFrequentFlyerMap");
                }

                TempData["update_message"] = "Update failed.";
                return RedirectToAction("ShowFrequentFlyerMap");
            }
        }

    }
}
