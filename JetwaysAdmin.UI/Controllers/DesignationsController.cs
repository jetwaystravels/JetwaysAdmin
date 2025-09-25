using JetwaysAdmin.Entity;
using JetwaysAdmin.UI.ApplicationUrl;
using JetwaysAdmin.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace JetwaysAdmin.UI.Controllers
{
    public class DesignationsController : Controller
    {
        public async Task<IActionResult> ShowDesignations(int IdLegal, string LegalEntityCode, string LegalEntityName)
        {
            ViewBag.LegalEntityCode = LegalEntityCode;
            ViewBag.LegalEntityName = LegalEntityName;
            ViewBag.Id = IdLegal;
            List<CustomerDesignation> customerDesignation = new List<CustomerDesignation>();
            List<CustomerDepartmentData> customerdepartment = new List<CustomerDepartmentData>();
            using (HttpClient client = new HttpClient())
            {
                string requestUrl = $"{AppUrlConstant.GetCustomerDesignation}?LegalEntityCode={LegalEntityCode}";
                var deaprtmentresponse = await client.GetAsync(requestUrl);
                if (deaprtmentresponse.IsSuccessStatusCode)
                {
                    var result = await deaprtmentresponse.Content.ReadAsStringAsync();
                    customerDesignation = JsonConvert.DeserializeObject<List<CustomerDesignation>>(result);
                }
                string RequestUrl = $"{AppUrlConstant.GetCustomerDepartment}?LegalEntityCode={LegalEntityCode}";
                var Deaprtmentresponse = await client.GetAsync(RequestUrl);
                if (Deaprtmentresponse.IsSuccessStatusCode)
                {
                    var result = await Deaprtmentresponse.Content.ReadAsStringAsync();
                    customerdepartment = JsonConvert.DeserializeObject<List<CustomerDepartmentData>>(result);
                }

            }
            var viewModel = new MenuHeaddata
            {
                Customerdesignation = customerDesignation,
                Customerdepartment = customerdepartment
            };
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddDesignation(CustomerDesignation customerdesignation, int IdLegal, string LegalEntityCode, string LegalEntityName)
        {
            ViewBag.LegalEntityCode = LegalEntityCode;
            ViewBag.LegalEntityName = LegalEntityName;
            ViewBag.Id = IdLegal;
            using (HttpClient client = new HttpClient())
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(customerdesignation);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsJsonAsync(AppUrlConstant.AddCustomerDesignation, customerdesignation);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    TempData["DesignationAdd"] = "Designation Add Successfully";
                }
            }
            ViewBag.ErrorMessage = "Data not  insert";
            return RedirectToAction("ShowDesignations", new { IdLegal = IdLegal, LegalEntityCode = LegalEntityCode, LegalEntityName = LegalEntityName });
        }
    }
}
