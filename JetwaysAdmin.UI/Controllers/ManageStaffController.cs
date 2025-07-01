using JetwaysAdmin.Entity;
using JetwaysAdmin.UI.ApplicationUrl;
using JetwaysAdmin.UI.Controllers.CustomeFilter;
using JetwaysAdmin.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace JetwaysAdmin.UI.Controllers
{
    public class ManageStaffController : Controller
    {

        [ServiceFilter(typeof(LogActionFilter))]
        public async Task<IActionResult> ShowManageStaff(string LegalEntityCode, string LegalEntityName)
        {
            ViewBag.LegalEntityCode = LegalEntityCode;
            ViewBag.LegalEntityName = LegalEntityName;
            List<InternalUsers> customeremployee = new List<InternalUsers>();
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(AppUrlConstant.GetInternalusers);
                if(response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    customeremployee = JsonConvert.DeserializeObject<List<InternalUsers>>(result);
                }
                var viewModel = new MenuHeaddata
                {
                    InternalUsers = customeremployee,
                };

                return View(viewModel);
            }
        }



        [HttpPost]
        [ServiceFilter(typeof(LogActionFilter))]
        public async Task<IActionResult> ManageStaff([FromForm] CustomerManageStaff customermanagestaff)
        {
          
            string employeeData = customermanagestaff.BookingConsultant;
            var employeeID = employeeData
            .Split(',', StringSplitOptions.RemoveEmptyEntries)       
            .Select(emp => emp.Split('-')[0].Trim())                 
            .ToList();

            customermanagestaff.BookingConsultant = string.Join(",", employeeID);

            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(customermanagestaff);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsJsonAsync(AppUrlConstant.ManageStaff, customermanagestaff);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    TempData["DataUpdate"] = "Update successfully";
                }
                ViewBag.ErrorMessage = "Data not  insert";
                return RedirectToAction("ShowManageStaff");
            }
        }


    }
}
