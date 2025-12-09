using JetwaysAdmin.Entity;
using JetwaysAdmin.UI.ApplicationUrl;
using JetwaysAdmin.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JetwaysAdmin.UI.Controllers.ViewComponents
{
    public class SidebarMenuViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int? Id,
        int? LegalEntityId,
        string LegalEntityName,
        string LegalEntityCode,
        int? EUserid)
        {
            List<MenuViewModel> menuItems = new List<MenuViewModel>();

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(AppUrlConstant.GetmenuList);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    menuItems = JsonConvert.DeserializeObject<List<MenuViewModel>>(result);
                }
            }
            ViewBag.Id = Id;
            ViewBag.LegalEntityId = LegalEntityId;
            ViewBag.LegalEntityName = LegalEntityName;
            ViewBag.LegalEntityCode = LegalEntityCode;
            ViewBag.EUserid = EUserid;
            return View(menuItems);
        }
    }
}
