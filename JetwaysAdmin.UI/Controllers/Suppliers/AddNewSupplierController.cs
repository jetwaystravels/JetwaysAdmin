using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using JetwaysAdmin.UI.ApplicationUrl;
using JetwaysAdmin.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace JetwaysAdmin.UI.Controllers.Suppliers
{
    public class AddNewSupplierController : Controller
    {
        
        public IActionResult ShowAddNewSupplier()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddNewSupplier([FromForm] AddSupplier addsupplier)
        {
            var file = Request.Form.Files.FirstOrDefault(f => f.Name == "Logo");
            if (file != null && file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    addsupplier.Logo = ms.ToArray(); 
                }
            }
            using (HttpClient client = new HttpClient())
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(addsupplier);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsJsonAsync(AppUrlConstant.AddSupplier, addsupplier);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    TempData["SupplierAdd"] = "Supplier Add Successfully";
                }
                ViewBag.ErrorMessage = "Data not  insert";
                return RedirectToAction("ShowAddNewSupplier");
            }
        }
        public  async Task<IActionResult> UpdateSupplier(int supplierId)
        {
            AddSupplier supplier = null;
            AddressCountryState CountryName = null;
            List<Country> countryList = new List<Country>();
            using (HttpClient client = new HttpClient())
            {
                string url = $"{AppUrlConstant.GetSupplierID}/{supplierId}";
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    supplier = JsonConvert.DeserializeObject<AddSupplier>(result);
                    ViewBag.SupplierId = supplierId;
                }
                var suppresponse = await client.GetAsync(AppUrlConstant.GetCountry);
                if (suppresponse.IsSuccessStatusCode)
                {
                    var Result = await suppresponse.Content.ReadAsStringAsync();
                    countryList = JsonConvert.DeserializeObject<List<Country>>(Result); 
                }
            }
            ViewBag.CountryList = countryList;
            return View(supplier);
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
        public async Task<IActionResult> EditSupplier(AddSupplier supplier)
        {
            var file = Request.Form.Files.FirstOrDefault(f => f.Name == "Logo");
            if (file != null && file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    supplier.Logo = ms.ToArray(); 
                }
            }
            using (HttpClient client = new HttpClient())
            {
                string Data = JsonConvert.SerializeObject(supplier);
                StringContent content = new StringContent(Data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(AppUrlConstant.EditSupplierID + "/" + supplier.SupplierId, content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["supplier_message"] = "Supplier Update";
                    return RedirectToAction("UpdateSupplier", new {supplierId = supplier.SupplierId });
                }
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Credentials(int supplierId)
        {
            List<SuppliersCredential> suppliersCredential = new List<SuppliersCredential>();
            using (HttpClient client = new HttpClient())
            {
                var userresponse = await client.GetAsync(AppUrlConstant.GetSupplierCredential);
                if (userresponse.IsSuccessStatusCode)
                {
                    var result = await userresponse.Content.ReadAsStringAsync();
                    suppliersCredential = JsonConvert.DeserializeObject<List<SuppliersCredential>>(result);
                }
            }
            var Supplierdata = new MenuHeaddata
            {
                supplierscredential = suppliersCredential
            };
            ViewBag.SupplierId = supplierId;
            return View(Supplierdata);
        }

        [HttpPost]
        public async Task<IActionResult> AddCredentials(int supplierId, SuppliersCredential suppliersCredential)
        {
            using (HttpClient client = new HttpClient())
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(suppliersCredential);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsJsonAsync(AppUrlConstant.AddSupplierCredential, suppliersCredential);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                }
            }
            ViewBag.SupplierId = supplierId;
            return RedirectToAction("Credentials");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCredential(int supplierId, int Id)
        {
            SuppliersCredential supplierscredential = null;
            using (HttpClient client = new HttpClient())
            {
                string url = $"{AppUrlConstant.GetSupplierCredentialID}/{Id}";
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    supplierscredential = JsonConvert.DeserializeObject<SuppliersCredential>(result);
                }
            }

            ViewBag.SupplierId = supplierId;
            return View(supplierscredential);
        }

        [HttpPost]
        public async Task<IActionResult> EditInternalUsers(SuppliersCredential supplierscredential)
        {
            using (HttpClient client = new HttpClient())
            {
                string Data = JsonConvert.SerializeObject(supplierscredential);
                StringContent content = new StringContent(Data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(AppUrlConstant.GetSupplierCredentialID + "/" + supplierscredential.Id, content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("UpdateCredential", new { Id = supplierscredential.Id });
                }
            }
            return RedirectToAction("UpdateCredential");
        }

        public IActionResult DealCodes(int supplierId)
        {
            ViewBag.SupplierId = supplierId;
            return View();
        } 
        public IActionResult NetRemitCodes(int supplierId)
        {
            ViewBag.SupplierId = supplierId;
            return View();
        }  
        public IActionResult AdvancePricingMarkup(int supplierId)
        {
            ViewBag.SupplierId = supplierId;
            return View();
        }  
        public IActionResult FareTypeSettings(int supplierId)
        {
            ViewBag.SupplierId = supplierId;
            return View();
        }
    }
}
