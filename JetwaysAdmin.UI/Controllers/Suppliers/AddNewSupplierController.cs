﻿using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using JetwaysAdmin.Repositories.Migrations;
using JetwaysAdmin.UI.ApplicationUrl;
using JetwaysAdmin.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
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
            ViewBag.SupplierId = supplierId;
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
        [HttpPost]
        public async Task<IActionResult> UpdateSupplierAppCode(AddSupplier supplier)
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

                    return RedirectToAction("ShowManageSuppliers", "ManageSuppliers");
                }
            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Credentials(int supplierId)
        {
   
            List<SuppliersCredential> suppliersCredential = new List<SuppliersCredential>();
            List<IATAGroupView> iataGroups = new List<IATAGroupView>();
            using (HttpClient client = new HttpClient())
            {
                var userresponse = await client.GetAsync(AppUrlConstant.GetSupplierCredential);
                if (userresponse.IsSuccessStatusCode)
                {
                    var result = await userresponse.Content.ReadAsStringAsync();
                    var allCredentialsdata = JsonConvert.DeserializeObject<List<SuppliersCredential>>(result);
                     suppliersCredential = allCredentialsdata
                        .Where(x => x.SupplierId != null && x.SupplierId.ToString() == supplierId.ToString())
                        .ToList();
                }
                var iataResponse = await client.GetAsync(AppUrlConstant.GetIATAGroup);
                if (iataResponse.IsSuccessStatusCode)
                {
                    var result = await iataResponse.Content.ReadAsStringAsync();
                    iataGroups = JsonConvert.DeserializeObject<List<IATAGroupView>>(result);
                }
            }
            var Supplierdata = new MenuHeaddata
            {
                supplierscredential = suppliersCredential,
                IATAGruopName = iataGroups
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
                    TempData["Add_Credentials"] = "Credentials add succefully";
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
                    TempData["Update_Credentials"] = "Credentials update succefully";
                    return RedirectToAction("UpdateCredential", new { Id = supplierscredential.Id });
                }
            }
            return RedirectToAction("UpdateCredential");
        }
        [HttpGet]
        public async Task<IActionResult> DealCodes(int supplierId)
        {
            List<DealCode> dealcode = new List<DealCode>();
            List<IATAGroupView> iataGroups = new List<IATAGroupView>();
            using (HttpClient client=new HttpClient())
            {
                var dealresponse = await client.GetAsync(AppUrlConstant.GetDealCode);
                if (dealresponse.IsSuccessStatusCode)
                {
                    var result = await dealresponse.Content.ReadAsStringAsync();
                    dealcode = JsonConvert.DeserializeObject<List<DealCode>>(result);
                }
                var iataResponse = await client.GetAsync(AppUrlConstant.GetIATAGroup);
                if (iataResponse.IsSuccessStatusCode)
                {
                    var result = await iataResponse.Content.ReadAsStringAsync();
                    iataGroups = JsonConvert.DeserializeObject<List<IATAGroupView>>(result);
                }
            }
            var dealCode = new MenuHeaddata
            {
                DealCodeView= dealcode,
                IATAGruopName= iataGroups

            };
            ViewBag.SupplierId = supplierId;
            return View(dealCode);
        }

        [HttpPost]
        public async Task<IActionResult> AddDealCodes(int supplierId, DealCode dealcode)
        {
           
            using (HttpClient client = new HttpClient())
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(dealcode);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsJsonAsync(AppUrlConstant.AddDealCode, dealcode);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    TempData["Add_DealCode"] = "DealCode add succefully";
                }
            }
            ViewBag.SupplierId = supplierId;
            return RedirectToAction("DealCodes");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateDealCode(int supplierId, int Id)
        {
            DealCode dealcode = null;
            using (HttpClient client = new HttpClient())
            {
                string url = $"{AppUrlConstant.GetDealCodeID}/{Id}";
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    dealcode = JsonConvert.DeserializeObject<DealCode>(result);
                }
            }

            ViewBag.SupplierId = supplierId;
            return View(dealcode);
        }
        [HttpPost]
        public async Task<IActionResult> EditDealCode(int supplierId, DealCode dealcode)
        {
            using (HttpClient client = new HttpClient())
            {
                string Data = JsonConvert.SerializeObject(dealcode);
                StringContent content = new StringContent(Data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(AppUrlConstant.GetDealCodeID + "/" + dealcode.DealCodeId, content);
                if (response.IsSuccessStatusCode)
                {
                    ViewBag.SupplierId = supplierId;
                    return RedirectToAction("UpdateDealCode", new { Id = dealcode.DealCodeId });
                }
              
            }
            return RedirectToAction("UpdateDealCode");
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
