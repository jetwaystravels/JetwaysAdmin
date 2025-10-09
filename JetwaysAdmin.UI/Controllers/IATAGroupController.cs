using Microsoft.AspNetCore.Mvc;

namespace JetwaysAdmin.UI.Controllers
{
    public class IATAGroupController : Controller
    {
        public async Task<IActionResult> ShowIATAGroup()
        {

            return View("IATAview");
        }

        public async Task<IActionResult> AddIATAGroup()
        {

            return View("AddIATAGroup");
        }
    }
}
