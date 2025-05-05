using JetwaysAdmin.Entity;
using JetwaysAdmin.UI.ApplicationUrl;
using JetwaysAdmin.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace JetwaysAdmin.UI.Controllers
{
    public class ManageStaffController : Controller
    {
        public async Task<IActionResult> ShowManageStaff()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ManageStaff([FromForm] CustomerManageStaff customermanagestaff)
        {
            using (HttpClient client = new HttpClient())
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(customermanagestaff);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsJsonAsync(AppUrlConstant.ManageStaff, customermanagestaff);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                }
                ViewBag.ErrorMessage = "Data not  insert";
                return RedirectToAction("ShowManageStaff");
            }
        }


    }
}
