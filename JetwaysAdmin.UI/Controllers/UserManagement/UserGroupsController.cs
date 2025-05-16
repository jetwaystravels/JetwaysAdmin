using Microsoft.AspNetCore.Mvc;

namespace JetwaysAdmin.UI.Controllers.UserManagement
{
    public class UserGroupsController : Controller
    {
        public IActionResult ShowUserGroups()
        {
            return View();
        }
    }
}
