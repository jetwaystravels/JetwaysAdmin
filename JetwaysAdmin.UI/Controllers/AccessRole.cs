using JetwaysAdmin.Entity;
using JetwaysAdmin.UI.ApplicationUrl;
using JetwaysAdmin.UI.Models;
using JetwaysAdmin.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.UI.Controllers
{
    public class AccessRoleController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AccessRoleController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET: /AccessRole/UserAccess?userId=10
        [HttpGet]
        public async Task<IActionResult> UserAccess(int userId)
        {
            var apiBaseUrl = "http://localhost:7260/";

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUrl);

                // 1) Load all admins for dropdown
                var userResp = await client.GetAsync("api/Admin/LogIn");
                var users = new List<Admin>();

                if (userResp.IsSuccessStatusCode)
                {
                    var usersJson = await userResp.Content.ReadAsStringAsync();
                    users = JsonConvert.DeserializeObject<List<Admin>>(usersJson) ?? new List<Admin>();
                }

                // 2) Load menu structure
                var menuResponse = await client.GetAsync("api/Menu/MenuListData");
                if (!menuResponse.IsSuccessStatusCode)
                {
                    // fallback empty model
                    return View(new UserMenuRightsVM
                    {
                        UserId = userId,
                        UserName = "User " + userId,
                        Rights = new List<MenuRightRowVM>(),
                        UserOptions = users.Select(u => new SelectListItem
                        {
                            // ⬇️ adjust property names if your Admin class is different
                            Value = u.admin_id.ToString(),
                            Text = u.admin_name,
                            Selected = (u.admin_id == userId)
                        }).ToList()
                    });
                    
                }

                var menuGroups = await menuResponse.Content.ReadFromJsonAsync<List<MenuGroupDto>>();

                // 3) Load saved rights for this user
                var rightsResponse = await client.GetAsync($"api/MenuRights/user/{userId}");
                var savedRights = rightsResponse.IsSuccessStatusCode
                    ? await rightsResponse.Content.ReadFromJsonAsync<List<MenuRightDto>>()
                    : new List<MenuRightDto>();

                var rightsLookup = savedRights.ToDictionary(r => r.MenuId, r => r);

                // 4) Build rights rows and pre-check boxes
                var rightsVm = new List<MenuRightRowVM>();

                foreach (var group in menuGroups.Where(g => g.IsActive))
                {
                    foreach (var sub in group.SubMenus.Where(s => s.IsActive))
                    {
                        var row = new MenuRightRowVM
                        {
                            MenuId = sub.Id,
                            HeaderTitle = group.Title,
                            ItemName = sub.Name,
                            CanView = false,
                            CanAdd = false,
                            CanEdit = false,
                            CanDelete = false
                        };

                        if (rightsLookup.TryGetValue(sub.Id, out var r))
                        {
                            row.CanView = r.CanView;
                            row.CanAdd = r.CanAdd;
                            row.CanEdit = r.CanEdit;
                            row.CanDelete = r.CanDelete;
                        }

                        rightsVm.Add(row);
                    }
                }

                // current selected user for title
                var currentUser = users.FirstOrDefault(u => u.admin_id == userId);
                var vm = new UserMenuRightsVM
                {
                    UserId = userId,
                    UserName = currentUser?.admin_name ?? ("User " + userId),
                    Rights = rightsVm,
                    UserOptions = users.Select(u => new SelectListItem
                    {
                        Value = u.admin_id.ToString(),
                        Text = u.admin_name,
                        Selected = (u.admin_id == userId)
                    }).ToList()
                };

                return View(vm);
            }
        }

        // POST: /AccessRole/SaveUserRights
        [HttpPost]
        public async Task<IActionResult> SaveUserRights(UserMenuRightsVM model)
        {
            using (HttpClient client = new HttpClient())
            {
                //if (!ModelState.IsValid)
                //{
                //    return View("UserAccess", model);
                //}

                // map VM -> DTO for API
                var dto = new UserMenuRightsDto
                {
                    UserId = model.UserId,
                    Rights = model.Rights.Select(r => new MenuRightDto
                    {
                        MenuId = r.MenuId,
                        CanView = r.CanView,
                        CanAdd = r.CanAdd,
                        CanEdit = r.CanEdit,
                        CanDelete = r.CanDelete
                    }).ToList()
                };

                HttpResponseMessage response = await client.PostAsJsonAsync(AppUrlConstant.Menuright, dto);

                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError(string.Empty, "Failed to save rights to server.");
                    return View("UserAccess", model);
                }

                TempData["SuccessMessage"] = "Menu rights saved successfully.";
                return RedirectToAction(nameof(UserAccess), new { userId = model.UserId });
            }
        }
    }
}
