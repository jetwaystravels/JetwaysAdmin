using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Text;

namespace JetwaysAdmin.UI.Controllers
{
    public class LoginViewController : Controller
    {
        public async Task<IActionResult> loginview()
        {

            using (HttpClient client = new HttpClient())
            {

                //var json = Newtonsoft.Json.JsonConvert.SerializeObject(loginRequest);
                //var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.GetAsync("http://localhost:7260/api/Admin/Login");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var jsonResult = JObject.Parse(result);

                    //var token = jsonResult["token"].ToString();
                    var adminUsername = jsonResult["username"].ToString();

                    // Store the token in session or cookie
                    // HttpContext.Session.SetString("JwtToken", token);
                    // HttpContext.Session.SetString("AdminUsername", adminUsername);

                    return RedirectToAction("Dashboard");  // Redirect to dashboard
                }

                ViewBag.ErrorMessage = "Invalid login credentials";
                return View();
            }
        }
    }
}
