using JetwaysAdmin.Entity;
using JetwaysAdmin.UI.ViewModel;
using JetwaysAdmin.UI.ApplicationUrl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using JetwaysAdmin.UI.Models;
using Microsoft.EntityFrameworkCore;

namespace JetwaysAdmin.UI.Controllers
{
    public class LoginController : Controller
    {
        public async Task<IActionResult> UserLogin()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> UserLogin(string username, string password)
        {
            using (HttpClient client = new HttpClient())
            {

                var loginRequest = new { Username = username, Password = password };
                var json = JsonConvert.SerializeObject(loginRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(AppUrlConstant.login, content);

                if (response.IsSuccessStatusCode)
                {
                    Admin _admin = new Admin();
                    var result = await response.Content.ReadAsStringAsync();
                    var JsonObj = JsonConvert.DeserializeObject<Admin>(result);
                    var userid = _admin.admin_id;

                    //var userid= JsonObj.
                    // Store the token in session or cookie
                    // HttpContext.Session.SetString("JwtToken", token);
                    //HttpContext.Session.SetString("AdminUsername", username);
                    //return RedirectToAction("LoginView");  // Redirect to dashboard
                    //return RedirectToAction("dashboard");
                    return RedirectToAction("Dashboard", new { userID = userid });
                }

                ViewBag.ErrorMessage = "Invalid login credentials";
                return View();
            }


        }

        [HttpGet]
        public async Task<IActionResult> dashboard()
        {

            Dashboard dashboard = new Dashboard();

            using (HttpClient client = new HttpClient())
            {
                var dashboardResponse = await client.GetAsync(AppUrlConstant.Dashboard);
                var supplierResponse = await client.GetAsync(AppUrlConstant.GetSupplier);
                var GetIATAGroupResponse = await client.GetAsync(AppUrlConstant.GetIATAGroup);

                if (dashboardResponse.IsSuccessStatusCode)
                {
                    var result = await dashboardResponse.Content.ReadAsStringAsync();

                    if (int.TryParse(result, out int customerCount))
                    {
                        dashboard.CustomerCount = customerCount;
                    }
                    else
                    {
                        
                    }
                }

                if (supplierResponse.IsSuccessStatusCode)
                {
                    var supplierResult = await supplierResponse.Content.ReadAsStringAsync();

                    try
                    {
                        var suppliers = JsonConvert.DeserializeObject<List<AddSupplier>>(supplierResult);
                        dashboard.SupplierCount = suppliers?.Count ?? 0;
                    }
                    catch (Exception ex)
                    {
                        
                        dashboard.SupplierCount = 0;
                    }
                }

                if (GetIATAGroupResponse.IsSuccessStatusCode)
                {
                    var GetIATAGroupResult = await GetIATAGroupResponse.Content.ReadAsStringAsync();

                    try
                    {
                        var GetIATAgroup = JsonConvert.DeserializeObject<List<AddSupplier>>(GetIATAGroupResult);
                        dashboard.IATAGroupsCount = GetIATAgroup?.Count ?? 0;
                    }
                    catch (Exception ex)
                    {

                        dashboard.IATAGroupsCount = 0;
                    }
                }

            }

            return View(dashboard);

        }
        public async Task<IActionResult> LoginView()
        {
            using (HttpClient client = new HttpClient())
            {

                var response = await client.GetAsync(AppUrlConstant.login);
                List<Admin> loginlist = new List<Admin>();
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    loginlist = JsonConvert.DeserializeObject<List<Admin>>(result);


                }
                return View(loginlist);
            }
        }


    }
}
