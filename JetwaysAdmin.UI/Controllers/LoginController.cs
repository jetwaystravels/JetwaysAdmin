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
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(loginRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(AppUrlConstant.login, content);

                if (response.IsSuccessStatusCode)
                {
                    Admin _admin =  new Admin();
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


        //public async Task<IActionResult> dashboard(int userID)
        //{

        //    userID = 1;
        //    using (HttpClient client = new HttpClient())
        //    {
        //        //userID = 2;
        //        var response = await client.GetAsync(AppUrlConstant.getAdminID + "?userID=" + userID );
             
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var result = await response.Content.ReadAsStringAsync();
        //            Admin _admin = new Admin();
        //            var JsonObj = JsonConvert.DeserializeObject<Admin>(result);
        //            _admin.admin_id = JsonObj.admin_id;
        //            _admin.admin_name = JsonObj.admin_name;
        //            _admin.admin_email= JsonObj.admin_email;
                   


        //            return View(_admin); // Redirect to dashboard
        //        }

        //        ViewBag.ErrorMessage = "Invalid login credentials";
        //        return View();
        //    }

        //}


        public async Task<IActionResult> dashboard(int userID)
        {

            userID = 1;
            using (HttpClient client = new HttpClient())
            {
                
                 var response = await client.GetAsync(AppUrlConstant.GetmenuList);
                List<MenuHeaddata> _menuItem = new List<MenuHeaddata>();

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                                
                     _menuItem = JsonConvert.DeserializeObject<List<MenuHeaddata>>(result);

                  //  return View(_menuItem); // Redirect to dashboard
                }

                //int customerCount = 0;
                //var customerCountResponse = await client.GetAsync(AppUrlConstant.getcustomercount); // Change to your real URL

                //if (customerCountResponse.IsSuccessStatusCode)
                //{
                //    var countResult = await customerCountResponse.Content.ReadAsStringAsync();
                //    customerCount = JsonConvert.DeserializeObject<int>(countResult); // assuming it returns plain int like 42
                //}

                //// 3. Pass both to the view via ViewModel
                //var viewModel = new DashboardViewModel
                //{
                //    MenuItems = _menuItem,
                //    CustomerCount = customerCount
                //};

                ViewBag.ErrorMessage = "Invalid login credentials";
                return View(_menuItem);
            }

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
