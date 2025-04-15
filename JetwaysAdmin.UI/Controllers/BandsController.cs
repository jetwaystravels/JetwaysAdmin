using JetwaysAdmin.UI.ApplicationUrl;
using JetwaysAdmin.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JetwaysAdmin.UI.Controllers
{
    public class BandsController : Controller
    {
        public async Task<IActionResult> ShowBands()
        {
            using (HttpClient client = new HttpClient())
            {

                var response = await client.GetAsync(AppUrlConstant.GetmenuList);
                List<MenuHeaddata> _menuItem = new List<MenuHeaddata>();

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    _menuItem = JsonConvert.DeserializeObject<List<MenuHeaddata>>(result);
                }
                ViewBag.ErrorMessage = "Invalid login credentials";
                return View(_menuItem);
            }
        }
    }
}
