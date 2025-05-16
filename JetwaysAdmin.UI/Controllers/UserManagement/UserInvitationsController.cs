using Microsoft.AspNetCore.Mvc;

namespace JetwaysAdmin.UI.Controllers.UserManagement
{
    public class UserInvitationsController : Controller
    {
        public IActionResult ShowUserInvitations()
        {
            return View();
        }
    }
}
