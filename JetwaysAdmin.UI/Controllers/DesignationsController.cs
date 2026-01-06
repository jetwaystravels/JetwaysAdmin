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
        public async Task<IActionResult> ShowDesignations()
        {
            //ViewBag.LegalEntityCode = LegalEntityCode;
            //ViewBag.LegalEntityName = LegalEntityName;
            //ViewBag.Id = IdLegal;
            List<CustomerDesignation> customerDesignation = new List<CustomerDesignation>();
            List<CustomerDepartmentData> customerdepartment = new List<CustomerDepartmentData>();
            using (HttpClient client = new HttpClient())
            {
               // string requestUrl = $"{AppUrlConstant.GetCustomerDesignation}?LegalEntityCode={LegalEntityCode}";
                string requestUrl = $"{AppUrlConstant.GetCustomerDesignation}";
                var deaprtmentresponse = await client.GetAsync(requestUrl);
                if (deaprtmentresponse.IsSuccessStatusCode)
                {
                    var result = await deaprtmentresponse.Content.ReadAsStringAsync();
                    customerDesignation = JsonConvert.DeserializeObject<List<CustomerDesignation>>(result);
                }
                //string RequestUrl = $"{AppUrlConstant.GetCustomerDepartment}?LegalEntityCode={LegalEntityCode}";
                string RequestUrl = $"{AppUrlConstant.GetCustomerDepartment}";
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
            return RedirectToAction("ShowDesignations");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateDesignation(int DesignationID)
        {
            CustomerDesignation entity = null;
            using (HttpClient client = new HttpClient())
            {
                string url = $"{AppUrlConstant.GetCustomerDesignationID}/{DesignationID}";
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    entity = JsonConvert.DeserializeObject<CustomerDesignation>(result);
                }
            }

            return View(entity);
        }
        [HttpPost]
        public async Task<IActionResult> EditDesignation(CustomerDesignation designationdata)
        {
            using (HttpClient client = new HttpClient())
            {

                string Data = JsonConvert.SerializeObject(designationdata);
                StringContent content = new StringContent(Data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync(AppUrlConstant.GetCustomerDesignationID + "/" + designationdata.DesignationID, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["Update_Designation"] = "Designation update successfully";
                    return RedirectToAction("UpdateDesignation", new
                    {
                        DesignationID = designationdata.DesignationID

                    });
                }
            }
            return View();
        }
    }
}
