using JetwaysAdmin.Entity;
using JetwaysAdmin.UI.ApplicationUrl;
using JetwaysAdmin.UI.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JetwaysAdmin.UI.Controllers.UserManagement
{
    public class ManagerUsersController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> ShowManagerUsers()
        {
            List<AddUser> manageuser = new List<AddUser>();
            using (HttpClient client = new HttpClient())
            {
                var userresponse = await client.GetAsync(AppUrlConstant.Manageuser);
                if (userresponse.IsSuccessStatusCode)
                {
                    var result = await userresponse.Content.ReadAsStringAsync();
                    manageuser = JsonConvert.DeserializeObject<List<AddUser>>(result);
                }
            }
            var viewModel = new MenuHeaddata
            {
                usermanage = manageuser,
            };

            return View(viewModel);
        }
    }
}
