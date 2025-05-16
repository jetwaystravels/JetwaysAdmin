using JetwaysAdmin.Entity;
using JetwaysAdmin.UI.ApplicationUrl;
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
            using (HttpClient client = new HttpClient())
            {
                string url = $"{AppUrlConstant.GetSupplierID}/{supplierId}";
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    supplier = JsonConvert.DeserializeObject<AddSupplier>(result);
                }
            }
             return View(supplier);
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
                    supplier.Logo = ms.ToArray(); // Assign image byte array to model
                }
            }
            using (HttpClient client = new HttpClient())
            {

                string Data = JsonConvert.SerializeObject(supplier);
                StringContent content = new StringContent(Data, Encoding.UTF8, "application/json");
                //HttpResponseMessage response = client.PutAsync(AppUrlConstant.EditSupplierID + "/" + supplier.SupplierId, content).Result;
                HttpResponseMessage response = await client.PutAsync(AppUrlConstant.EditSupplierID + "/" + supplier.SupplierId, content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["supplier_message"] = "Supplier Update";
                    return RedirectToAction("UpdateSupplier", new {supplierId = supplier.SupplierId });
                   
                }
            }
            return View();
        }




    }
}
