using JetwaysAdmin.UI.ViewModel;
using JetwaysAdmin.UI.ApplicationUrl;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace JetwaysAdmin.UI.Controllers
{
    public class MenuList : Controller
    {
        public async Task<IActionResult> MenuListView()
        {

            //var menus = await _menuRepository.GetAllMenusAsync();

            using (HttpClient client = new HttpClient())
            {
                MenuItemdata _MenuItem = new MenuItemdata();

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(_MenuItem);
                var content = new StringContent(json, Encoding.UTF8, "application/json");



                // Make the POST request
                HttpResponseMessage response = await client.GetAsync(AppUrlConstant.getmenu);

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
