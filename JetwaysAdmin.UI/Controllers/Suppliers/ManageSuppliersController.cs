using JetwaysAdmin.Entity;
using JetwaysAdmin.UI.ApplicationUrl;
using JetwaysAdmin.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JetwaysAdmin.UI.Controllers.Suppliers
{
    public class ManageSuppliersController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> ShowManageSuppliers()
        {
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
                getsupplier = supplier,
            };

            return View(viewModel);
        }

       
    }
}
