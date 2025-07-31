using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using JetwaysAdmin.UI.ApplicationUrl;
using JetwaysAdmin.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

namespace JetwaysAdmin.UI.Controllers
{
    public class SuppliersDealCodesController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> ShowSuppliersDealCodes(int Id, string LegalEntityCode, string LegalEntityName)
        {
            ViewBag.LegalEntityCode = LegalEntityCode;
            ViewBag.LegalEntityName = LegalEntityName;
            ViewBag.Id = Id;
            List<AddSupplier> supplier = new List<AddSupplier>();
            using (HttpClient client = new HttpClient())
            {
                //var suppresponse = await client.GetAsync(AppUrlConstant.GetSupplier);
                var url = $"{AppUrlConstant.GetSuppliersLegalEntity}?legalEntityCode={LegalEntityCode}";
                var suppresponse = await client.GetAsync(url);
             //   var suppresponse = await client.GetAsync(AppUrlConstant.GetSuppliersLegalEntity);
                if (suppresponse.IsSuccessStatusCode)
                {
                    var result = await suppresponse.Content.ReadAsStringAsync();
                    supplier = JsonConvert.DeserializeObject<List<AddSupplier>>(result);
                }
            }
            var viewModel = new MenuHeaddata
            {
                getsupplier = supplier
            };

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateSupplierAppStatus(string legalEntityCode, int supplierId, bool appStatus, string legalEntityName)
        {
            using (HttpClient client = new HttpClient())
            {
                // Prepare payload
                var payload = new
                {
                    LegalEntityCode = legalEntityCode,
                    SupplierID = supplierId,
                    IsActive = appStatus
                };

                string json = JsonConvert.SerializeObject(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Correct POST URL (no query params)
                string url = $"{AppUrlConstant.Updatelegalentitysupplierstatus}";
                HttpResponseMessage response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["supplier_message"] = "Status Updated";
                    return RedirectToAction("ShowSuppliersDealCodes", new { LegalEntityCode = legalEntityCode, LegalEntityName = legalEntityName });
                }
            }

            TempData["supplier_message"] = "Failed to update status";
            return RedirectToAction("ShowSuppliersDealCodes", new { LegalEntityCode = legalEntityCode, LegalEntityName = legalEntityName });
        }





        [HttpGet]
        //public async Task<IActionResult> GetSupplierCredential(int SupplierId,int Id, string LegalEntityCode, string LegalEntityName)
        //{
        //    List<SuppliersCredential> allCredentials = new List<SuppliersCredential>();
        //    List<SuppliersCredential> filteredCredentials = new List<SuppliersCredential>();
        //    List<DealCode> dealcode = new List<DealCode>();
        //    List<DealCode> filtereddealcode = new List<DealCode>();
        //    using (HttpClient client = new HttpClient())
        //    {
        //        string url = $"{AppUrlConstant.GetSupplierCredential}";
        //        var response = await client.GetAsync(url);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var result = await response.Content.ReadAsStringAsync();
        //            allCredentials = JsonConvert.DeserializeObject<List<SuppliersCredential>>(result);
        //            filteredCredentials = allCredentials
        //           .Where(c => int.TryParse(c.SupplierCode, out int code) && code == SupplierId)
        //             .ToList();
        //        }

        //        url = $"{AppUrlConstant.GetDealCodeSupplierId}/?SupplierId={SupplierId}";

        //        var dealresponse = await client.GetAsync(url);
        //        if (dealresponse.IsSuccessStatusCode)
        //        {
        //            var result = await dealresponse.Content.ReadAsStringAsync();
        //            dealcode = JsonConvert.DeserializeObject<List<DealCode>>(result);
        //            filtereddealcode = dealcode
        //            .Where(c => int.TryParse(c.SupplierCode, out int code) && code == SupplierId)
        //             .ToList();
        //        }
        //    }
        //    var dealCode = new MenuHeaddata
        //    {
        //        supplierscredential = filteredCredentials,
        //        DealCodeView = filtereddealcode

        //    };
        //    ViewBag.SupplierCode = SupplierId;
        //    ViewBag.LegalEntityCode = LegalEntityCode;
        //    ViewBag.LegalEntityName = LegalEntityName;
        //    ViewBag.Id = Id;
        //    return View(dealCode); 
        //}
        //public async Task<IActionResult> UpdateSupplierCredential(int Id, string Code, string Name)
        //{
        //    SuppliersCredential credentailupdate = null;
        //    using (HttpClient client = new HttpClient())
        //    {
        //        string url = $"{AppUrlConstant.GetSupplierCredentialID}/{Id}";
        //        var response = await client.GetAsync(url);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var result = await response.Content.ReadAsStringAsync();
        //            credentailupdate = JsonConvert.DeserializeObject<SuppliersCredential>(result);
        //        }
        //    }
        //    ViewBag.LegalEntityCode = Code;
        //    ViewBag.LegalEntityName = Name;
        //    return View(credentailupdate);
        //}
        [HttpPost]
        public async Task<IActionResult> EditSupplierCredential(SuppliersCredential supplierscredential,int Id, string LegalEntityCode, string LegalEntityName)
        {
           using (HttpClient client = new HttpClient())
            {
                string Data = JsonConvert.SerializeObject(supplierscredential);
                StringContent content = new StringContent(Data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync(AppUrlConstant.GetSupplierCredentialID + "/" + supplierscredential.Id, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetSupplierCredential", new
                    {
                        SupplierId = supplierscredential.SupplierId,
                        LegalEntityCode = LegalEntityCode,
                        LegalEntityName = LegalEntityName,
                        Id=Id
                    });
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCredentials(int supplierId, SuppliersCredential suppliersCredential,int Id, string LegalEntityCode, string LegalEntityName)
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
            
            return RedirectToAction("GetSupplierCredential", new
            {
                SupplierId = supplierId,
                LegalEntityCode = LegalEntityCode,
                LegalEntityName = LegalEntityName,
                Id = Id
            });
        }
        [HttpPost]
        public async Task<IActionResult> AddDealCodes(int supplierId, DealCode dealcode,int Id, string LegalEntityCode, string LegalEntityName)
        {
             using (HttpClient client = new HttpClient())
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(dealcode);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsJsonAsync(AppUrlConstant.AddDealCode, dealcode);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("GetSupplierCredential", new
            {
                SupplierId = supplierId,
                LegalEntityCode = LegalEntityCode,
                LegalEntityName = LegalEntityName,
                Id = Id
            });
        }
    }
}
