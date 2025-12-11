using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using JetwaysAdmin.UI.ApplicationUrl;
using JetwaysAdmin.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace JetwaysAdmin.UI.Controllers
{
    public class DepartmentsController : Controller
    {
        public async Task<IActionResult> ShowDepartments()
        {
            
            List<CustomerDepartmentData> customerdepartment = new List<CustomerDepartmentData>();
            using (HttpClient client = new HttpClient())
            {
                //string requestUrl = $"{AppUrlConstant.GetCustomerDepartment}?LegalEntityCode={LegalEntityCode}";
                string requestUrl = $"{AppUrlConstant.GetCustomerDepartment}";
                var deaprtmentresponse = await client.GetAsync(requestUrl);
                if (deaprtmentresponse.IsSuccessStatusCode)
                {
                    var result = await deaprtmentresponse.Content.ReadAsStringAsync();
                     customerdepartment = JsonConvert.DeserializeObject<List<CustomerDepartmentData>>(result);
                }
            }
            var viewModel = new MenuHeaddata
            {
                Customerdepartment = customerdepartment,
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment(CustomerDepartmentData customerdepartment)
        {
           
            using (HttpClient client = new HttpClient())
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(customerdepartment);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsJsonAsync(AppUrlConstant.AddCustomerDepartment, customerdepartment);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    TempData["DepartmentAdd"] = "Department Add Successfully";
                }
            }
            ViewBag.ErrorMessage = "Data not  insert";
            return RedirectToAction("ShowDepartments");
        }


    }
}
