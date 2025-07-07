using JetwaysAdmin.Entity;
using JetwaysAdmin.UI.ApplicationUrl;
using JetwaysAdmin.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace JetwaysAdmin.UI.Controllers
{
    public class SuppliersDealCodesController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> ShowSuppliersDealCodes(string LegalEntityCode, string LegalEntityName)
        {
            ViewBag.LegalEntityCode = LegalEntityCode;
            ViewBag.LegalEntityName = LegalEntityName;

            List<AddSupplier> supplier = new List<AddSupplier>();
            using (HttpClient client = new HttpClient())
            {
                var suppresponse = await client.GetAsync(AppUrlConstant.GetSupplier);
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
        [HttpGet]
        public async Task<IActionResult> GetSupplierCredential(int SupplierId, string LegalEntityCode, string LegalEntityName)
        {
            int Id = SupplierId;
            SuppliersCredential suppliercode = null;
            using (HttpClient client = new HttpClient())
            {
                string url = $"{AppUrlConstant.GetSupplierCredentialID}/{Id}";
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    // Assuming SuppliersCredential is a single object, not a list
                    var supplierCredential = JsonConvert.DeserializeObject<SuppliersCredential>(result);

                    if (supplierCredential != null)
                    {
                        // Assuming you want to return this object to the view
                        return View(supplierCredential);
                    }
                }
            }
            ViewBag.LegalEntityCode = LegalEntityCode;
            ViewBag.LegalEntityName = LegalEntityName;
            return View(suppliercode); 
        }
    }
}
