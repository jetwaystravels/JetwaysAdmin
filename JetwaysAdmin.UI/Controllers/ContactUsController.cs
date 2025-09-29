using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using JetwaysAdmin.UI.ApplicationUrl;
using JetwaysAdmin.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace JetwaysAdmin.UI.Controllers
{
    public class ContactUsController : Controller
    {
     public async Task<IActionResult> ShowContactUs(int? LegalEntityId, string LegalEntityCode, string LegalEntityName,  int? UserID, string empId)
     {
        ViewBag.LegalEntityCode = LegalEntityCode;
        ViewBag.LegalEntityName = LegalEntityName;
        ViewBag.LegalEntityId = LegalEntityId;
        ViewBag.EUserid = UserID;
        ViewBag.empId = empId;

        return View(new
        {
            LegalEntityName = LegalEntityName,
            LegalEntityCode = LegalEntityCode,
            LegalEntityId = LegalEntityId,
            UserID = UserID,
            empId = empId
        });
     }

        [HttpPost]
        public async Task<IActionResult> AddContactUs( [FromBody] ContactUsDetails contactus,string LegalEntityCode,string LegalEntityName,int? LegalEntityId,int? UserID, string empId)
        {
            ViewBag.LegalEntityCode = LegalEntityCode;
            ViewBag.LegalEntityName = LegalEntityName;

            using (HttpClient client = new HttpClient())
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(contactus);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsJsonAsync(AppUrlConstant.AddContactUS, contactus);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    TempData["AddContactUs"] = "User Add Successfully";
                }
                else
                {
                    TempData["AddContactUs"] = "Error saving Contact details";
                }

                return RedirectToAction("ShowContactUs", new
                {
                    LegalEntityId = LegalEntityId,
                    LegalEntityName = LegalEntityName,
                    LegalEntityCode = LegalEntityCode,
                    UserID = UserID,       
                    empId = empId         
                });
            }
        }

    }
}
