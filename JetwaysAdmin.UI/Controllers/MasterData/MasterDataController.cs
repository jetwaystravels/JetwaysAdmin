using Microsoft.AspNetCore.Mvc;

namespace JetwaysAdmin.UI.Controllers.MasterData
{
    public class MasterDataController : Controller
    {
        public IActionResult ShowMasterData()
        {
            return RedirectToAction("ShowDepartments", "Departments");
        }
    }
}
