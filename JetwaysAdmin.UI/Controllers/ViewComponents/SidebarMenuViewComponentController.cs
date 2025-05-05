using JetwaysAdmin.UI.ApplicationUrl;
using JetwaysAdmin.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JetwaysAdmin.UI.Controllers.ViewComponents
{
    public class SidebarMenuViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<MenuHeaddata> menuItems = new List<MenuHeaddata>();

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(AppUrlConstant.GetmenuList);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    menuItems = JsonConvert.DeserializeObject<List<MenuHeaddata>>(result);
                }
            }
            return View(menuItems);
        }
    }
}
