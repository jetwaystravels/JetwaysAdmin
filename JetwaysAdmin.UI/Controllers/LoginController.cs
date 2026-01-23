using JetwaysAdmin.Entity;
using JetwaysAdmin.UI.ViewModel;
using JetwaysAdmin.UI.ApplicationUrl;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using JetwaysAdmin.Repositories.Interface;
using JetwaysAdmin.UI.Models;
using Microsoft.Extensions.Options;

namespace JetwaysAdmin.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly AccessControlSettings _settings;
        public LoginController(IOptions<AccessControlSettings> settings)
        {
            _settings = settings.Value;
        }

        [HttpGet]
        public IActionResult UserLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserLogin(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.ErrorMessage = "Username and password are required.";
                return View();
            }

            using (HttpClient client = new HttpClient())
            {
                var loginRequest = new { Username = username, Password = password };
                var json = JsonConvert.SerializeObject(loginRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(AppUrlConstant.login, content);

                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.ErrorMessage = "Invalid login credentials";
                    return View();
                }

                var result = await response.Content.ReadAsStringAsync();

                // Expecting something like: { "id": 123, "name":"Ashok", ... }
                JObject jsonResult = JObject.Parse(result);

                var userId = (jsonResult["id"] ?? jsonResult["userId"] ?? jsonResult["admin_id"])?.ToString() ?? "";
                var name = (jsonResult["name"] ?? jsonResult["fullName"] ?? jsonResult["username"])?.ToString() ?? username;
                string userType;

                if (name.Equals("SuperAdmin"))
                {
                    userType = name;
                }
                else
                {
                    userType = name;
                }
              
                
                // Build claims
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Name, name),
                    new Claim(ClaimTypes.Email, username),
                    // If the API returns a role/type, replace this with that value:
                    new Claim("UserType", userType)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    new AuthenticationProperties
                    {
                        IsPersistent = true, // set to false if you don’t want a persistent cookie
                        AllowRefresh = true
                    });

                return RedirectToAction(nameof(Dashboard), new { userID = userId });
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var dashboard = new Dashboard();
            var email = User.Identity.Name;

            bool canAccessRole = _settings.AccessRoleUsers != null &&
                                 _settings.AccessRoleUsers
                                     .Any(u => u.Equals(email, StringComparison.OrdinalIgnoreCase));

            ViewBag.CanAccessRole = canAccessRole;

            using (HttpClient client = new HttpClient())
            {
                var dashboardResponse = await client.GetAsync(AppUrlConstant.Dashboard);
                var supplierResponse = await client.GetAsync(AppUrlConstant.GetSupplier);
                var getIATAGroupResponse = await client.GetAsync(AppUrlConstant.GetIATAGroup);

                if (dashboardResponse.IsSuccessStatusCode)
                {
                    var result = await dashboardResponse.Content.ReadAsStringAsync();
                    if (int.TryParse(result, out int customerCount))
                        dashboard.CustomerCount = customerCount;
                }

                if (supplierResponse.IsSuccessStatusCode)
                {
                    var supplierResult = await supplierResponse.Content.ReadAsStringAsync();
                    try
                    {
                        var suppliers = JsonConvert.DeserializeObject<List<AddSupplier>>(supplierResult);
                        dashboard.SupplierCount = suppliers?.Count ?? 0;
                    }
                    catch
                    {
                        dashboard.SupplierCount = 0;
                    }
                }

                if (getIATAGroupResponse.IsSuccessStatusCode)
                {
                    var getIATAGroupResult = await getIATAGroupResponse.Content.ReadAsStringAsync();
                    try
                    {
                        var iataGroups = JsonConvert.DeserializeObject<List<AddSupplier>>(getIATAGroupResult);
                        dashboard.IATAGroupsCount = iataGroups?.Count ?? 0;
                    }
                    catch
                    {
                        dashboard.IATAGroupsCount = 0;
                    }
                }
            }

            return View(dashboard);
        }

        [HttpGet]
        public async Task<IActionResult> LoginView()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(AppUrlConstant.login);
                var loginList = new List<Admin>();

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    loginList = JsonConvert.DeserializeObject<List<Admin>>(result) ?? new List<Admin>();
                }

                return View(loginList);
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View(new ChangePasswordViewModel());
        }

        /// <summary>
        /// Change password via your backend API (NOT ASP.NET Identity).
        /// Assumes an endpoint like AppUrlConstant.ChangePassword that accepts:
        /// { username/email (from claims), currentPassword, newPassword }
        /// </summary>
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var email = User.FindFirstValue(ClaimTypes.Email) ?? "";
            if (string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError(string.Empty, "Unable to resolve current user email.");
                return View(model);
            }

            using (HttpClient client = new HttpClient())
            {
                var request = new
                {
                    Username = email,                // or "Email" as your API expects
                  //  CurrentPassword = model.CurrentPassword,
                    NewPassword = model.NewPassword
                };

                var payload = JsonConvert.SerializeObject(request);
                var content = new StringContent(payload, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(AppUrlConstant.ChangePassword, content);

                if (response.IsSuccessStatusCode)
                {
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                    TempData["Toast.Type"] = "success";
                    TempData["Toast.Message"] = "Password changed successfully. Please log in with your new password.";
                    //return RedirectToAction("UserLogin", "Login");
                    return RedirectToAction("UserLogin", "Login");
                }

                var apiError = await response.Content.ReadAsStringAsync();
                TempData["Toast.Type"] = "danger";
                TempData["Toast.Message"] = string.IsNullOrWhiteSpace(apiError)
                    ? "Failed to change password."
                    : apiError;

                // Send them back to the change-password view (or wherever you want)
                return RedirectToAction("ChangePassword", "Login");

            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(UserLogin));
        }

        //[HttpGet]
        //public IActionResult Register()
        //{
        //    return View(new Admin());
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Admin model, [FromServices] EncryptionService encryptionService)
        {
            if (string.IsNullOrWhiteSpace(model.admin_name) ||
                string.IsNullOrWhiteSpace(model.admin_email) ||
                string.IsNullOrWhiteSpace(model.admin_password))
            {
                ViewBag.ErrorMessage = "Name, Email and Password are required.";
                return View(model);
            }

            // Encrypt password before sending to API / saving
            model.admin_password = encryptionService.Encrypt(model.admin_password);

            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // You must have this endpoint in your API
                var response = await client.PostAsync(AppUrlConstant.RegisterAdmin, content);

                if (!response.IsSuccessStatusCode)
                {
                    var apiError = await response.Content.ReadAsStringAsync();
                    ViewBag.ErrorMessage = string.IsNullOrWhiteSpace(apiError)
                        ? "Registration failed."
                        : apiError;

                    return View(model);
                }

                TempData["Toast.Type"] = "success";
                TempData["Toast.Message"] = "Registration successful. Please login.";

                return RedirectToAction(nameof(UserLogin));
            }
        }


    }
}
