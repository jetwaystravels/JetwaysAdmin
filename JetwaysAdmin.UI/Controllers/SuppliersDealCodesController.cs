using JetwaysAdmin.UI.ApplicationUrl;
using JetwaysAdmin.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JetwaysAdmin.UI.Controllers
{
    public class SuppliersDealCodesController : Controller
    {
        public async Task<IActionResult> ShowSuppliersDealCodes()
        {
            return View();
        }
    }
}
