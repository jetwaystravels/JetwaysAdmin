using Microsoft.AspNetCore.Mvc;

namespace JetwaysAdmin.UI.Controllers.UserManagement
{
    public class GuestUsersController : Controller
    {
        public IActionResult ShowGuestUsers()
        {
            return View();
        }
    }
}
