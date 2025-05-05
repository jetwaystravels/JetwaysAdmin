using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using JetwaysAdmin.UI.ApplicationUrl;
using JetwaysAdmin.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace JetwaysAdmin.UI.Controllers
{
    public class AccountBalanceController : Controller
    {
        public async Task<IActionResult> ShowAccountBalance()
        {
           
            return View();

        }


        [HttpPost]
        public async  Task<IActionResult> AddAccountBalance([FromForm] CustomerAccountBalance accountbalance)
        {
            using (HttpClient client = new HttpClient())
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(accountbalance);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsJsonAsync(AppUrlConstant.AddAccountBalance, accountbalance);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                }
                ViewBag.ErrorMessage = "Data not  insert";
                return RedirectToAction("ShowAccountBalance");
            }
        }
    }
}
