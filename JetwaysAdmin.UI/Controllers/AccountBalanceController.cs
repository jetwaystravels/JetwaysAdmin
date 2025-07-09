using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using JetwaysAdmin.Repositories.Migrations;
using JetwaysAdmin.UI.ApplicationUrl;
using JetwaysAdmin.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace JetwaysAdmin.UI.Controllers
{
    public class AccountBalanceController : Controller
    {
        public async Task<IActionResult> ShowAccountBalance(int Id,string LegalEntityCode, string LegalEntityName)
        {
            ViewBag.LegalEntityCode = LegalEntityCode;
            ViewBag.LegalEntityName = LegalEntityName;
            ViewBag.Id = Id;
            return View();

        }


        [HttpPost]
        public async Task<IActionResult> AddAccountBalance([FromForm] CustomerAccountBalance accountbalance, int Id, string LegalEntityCode, string LegalEntityName)
        {
           using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(accountbalance);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsJsonAsync(AppUrlConstant.AddAccountBalance, accountbalance);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                }
                ViewBag.ErrorMessage = "Data not  insert";
                return RedirectToAction("ShowAccountBalance", new
                {
                    LegalEntityCode = LegalEntityCode,
                    LegalEntityName = LegalEntityName,
                    Id=Id
                });
            }
        }
    }
}
