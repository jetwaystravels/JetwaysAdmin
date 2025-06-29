﻿using JetwaysAdmin.Entity;
using JetwaysAdmin.UI.ApplicationUrl;
using JetwaysAdmin.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace JetwaysAdmin.UI.Controllers.UserManagement
{
    public class UsersController : Controller
    {
        
        
        [HttpGet]
        public async Task<IActionResult> ShowUsers(string legalEntityCode, string legalEntityName, string legalEntityId, int UserID)
        {
            List<CustomersEmployee> customerdetail = new List<CustomersEmployee>();
            using (HttpClient client = new HttpClient())
            {
                var userdetail = await client.GetAsync(AppUrlConstant.GetCustomerEmployee);
                if (userdetail.IsSuccessStatusCode)
                {
                    var result = await userdetail.Content.ReadAsStringAsync();
                    customerdetail = JsonConvert.DeserializeObject<List<CustomersEmployee>>(result);
                }
            }
            var filteredUsers = customerdetail
              .Where(u => u.LegalEntityCode != null && u.LegalEntityCode.Equals(legalEntityCode, StringComparison.OrdinalIgnoreCase))
              .ToList();

            var userAlldetail = new MenuHeaddata
            {
                customersemployee = filteredUsers,
            };
            TempData["LegalEntityId"] = legalEntityId;
            TempData["LegalEntityName"] = legalEntityName;
            TempData["LegalEntityCode"] = legalEntityCode;
            ViewBag.EUserid = UserID;
            return View(userAlldetail);
        }

        public async  Task<IActionResult> updateUser(int UserID)
        {
           

            CustomersEmployee users = null;
            using (HttpClient client = new HttpClient())
            {
                string url = $"{AppUrlConstant.GetCustomerEmployeeID}/{UserID}";
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    users = JsonConvert.DeserializeObject<CustomersEmployee>(result);
                }
            }
            if (users != null)
            {

                if (users.Logo != null && users.Logo.Length < 180_000)
                {
                    string base64Logo = Convert.ToBase64String(users.Logo);
                    TempData["LogoBase64"] = base64Logo;
                }
                else
                {
                    TempData["LogoBase64"] = null;
                }

                TempData["EmplID"] = users.UserID;
                TempData["EmplName"] = $"{users.FirstName} {users.LastName}";
                ViewBag.EUserid = users.UserID;
            }
            return View(users);
        }



        [HttpPost]
        public async Task<IActionResult> EditUsers(CustomersEmployee users)
        {
            var file = Request.Form.Files.FirstOrDefault(f => f.Name == "Logo");

            if (file != null && file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    users.Logo = ms.ToArray();
                }
            }
            using (HttpClient client = new HttpClient())
            {

                string Data = JsonConvert.SerializeObject(users);
                StringContent content = new StringContent(Data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(AppUrlConstant.GetCustomerEmployeeID + "/" + users.UserID, content);
                if (response.IsSuccessStatusCode)
                {
                   
                    return RedirectToAction("updateUser", new { UserID = users.UserID });


                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> OrganizationUsersProfile(int UserID)
        {
            var legalEntityId = TempData["LegalEntityId"] as string;
            var legalEntityName = TempData["LegalEntityName"] as string;
            var legalEntityCode = TempData["LegalEntityCode"] as string;
            TempData.Keep("LegalEntityId");
            TempData.Keep("LegalEntityName");
            TempData.Keep("LegalEntityCode");
            List<LegalEntity> legalEntities = new List<LegalEntity>();
            MenuHeaddata legaldata = new MenuHeaddata();
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(AppUrlConstant.GetLegalEntity);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<LegalEntityResponse>(result);
                    legalEntities = responseData?.Data ?? new List<LegalEntity>();
                    var filteredEntities = legalEntities
                       .Where(le =>
                             le.LegalEntityCode == legalEntityCode ||
                           le.ParentLegalEntityCode == legalEntityCode)
                    .ToList();
                    legaldata.LegalEntitydata = filteredEntities;
                    ViewBag.EUserid = UserID;
                }
            }
            return View(legaldata);
        }





        [HttpPost]
        public async Task<IActionResult> AddUsers([FromForm] CustomersEmployee adduser)
        {
            var file = Request.Form.Files.FirstOrDefault(f => f.Name == "Logo");

            if (file != null && file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    adduser.Logo = ms.ToArray();
                }
            }

            using (HttpClient client = new HttpClient())
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(adduser);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsJsonAsync(AppUrlConstant.AddCustomerEmployee, adduser);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    TempData["AddUSers"] = "User Add Successfully";
                }
                ViewBag.ErrorMessage = "Data not  insert";
                return RedirectToAction("ShowUsers");
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddBillingEntity(EmployeeBillingEntity billingentity, int UserID)
        {
          

            using (HttpClient client = new HttpClient())
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(billingentity);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsJsonAsync(AppUrlConstant.AddBillingEntity, billingentity);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    TempData["Addbillingentity"] = "User Add Successfully";
                    ViewBag.EUserid = UserID;
                }
                else
                {
                    TempData["DuplicateBillingEntity"] = "A billing entity for this user already exists!";
                }
                ViewBag.ErrorMessage = "Data not  insert";
                return RedirectToAction("ShowUsers");
            }
        }



    }
}

