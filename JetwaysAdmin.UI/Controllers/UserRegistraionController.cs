using JetwaysAdmin.Entity;
using JetwaysAdmin.UI.ApplicationUrl;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Text;

namespace JetwaysAdmin.UI.Controllers
{
    public class UserRegistraionController : Controller
    {
        public IActionResult RegistraionData()
        {
            return View();
        }

        [HttpPost]
        // public async Task<IActionResult> Registraion(string admin_name, string admin_email,string admin_password, string admin_image)
        public async Task<IActionResult> Registraion(Admin _admin)
        {



            using (HttpClient client = new HttpClient())
            {
                Admin admin = _admin;

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(_admin);
                var content = new StringContent(json, Encoding.UTF8, "application/json");



                // Make the POST request
                HttpResponseMessage response = await client.PostAsJsonAsync(AppUrlConstant.AddAdmin, admin);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    TempData["SuccessMessage"] = "Data saved successfully!";
                    return RedirectToAction("Registraion");

                }
                var errorContent = await response.Content.ReadAsStringAsync();
                TempData["ErrorMessage"] = "Failed to save data. Please try again.";
                return View();
            }


        }
    }
}
