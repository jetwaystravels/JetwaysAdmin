using JetwaysAdmin.UI.ApplicationUrl;
using JetwaysAdmin.UI.ViewModel;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace JetwaysAdmin.UI.Controllers
{
    public class CustomerController : Controller
    {
        public async Task<IActionResult> Customerdata()
        {
            using (HttpClient client = new HttpClient())
            {
                MenuItemdata _MenuItem = new MenuItemdata();

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(_MenuItem);
                var content = new StringContent(json, Encoding.UTF8, "application/json");



                // Make the POST request
                HttpResponseMessage response = await client.GetAsync(AppUrlConstant.getcustomercount);

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



            return View();
        }
    }
}
