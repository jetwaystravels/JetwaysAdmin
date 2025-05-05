using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using JetwaysAdmin.UI.ApplicationUrl;
using JetwaysAdmin.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace JetwaysAdmin.UI.Controllers.UserManagement
{
    public class AddUserController : Controller
    {
        public async Task<IActionResult> ShowAddUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUserDetail(AddUser adduser)
        {
            using (HttpClient client = new HttpClient())
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(adduser);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsJsonAsync(AppUrlConstant.AddNewUser, adduser);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    TempData["UserAdd"] = "User Add Successfully";
                }
                ViewBag.ErrorMessage = "Data not  insert";
                return RedirectToAction("ShowAddUser");
            }
        }

    }
}
