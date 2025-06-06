using JetwaysAdmin.Entity;
using JetwaysAdmin.UI.ApplicationUrl;
using JetwaysAdmin.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

namespace JetwaysAdmin.UI.Controllers.UserManagement
{
    public class InternalUsersController : Controller
    {

        [HttpGet]
        public async Task<IActionResult> ShowInternalUsers()
        {
            List<InternalUsers> internaluser = new List<InternalUsers>();
            using (HttpClient client = new HttpClient())
            {
                var userresponse = await client.GetAsync(AppUrlConstant.GetInternalusers);
                if (userresponse.IsSuccessStatusCode)
                {
                    var result = await userresponse.Content.ReadAsStringAsync();
                    internaluser = JsonConvert.DeserializeObject<List<InternalUsers>>(result);
                    
                }
            }
            
            var viewModel = new MenuHeaddata
            {
                InternalUsers = internaluser,
            };

            return View(viewModel);
        }



        [HttpPost]
        public async Task<IActionResult> AddInternalUsers([FromForm]  InternalUsers adduser)
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
                HttpResponseMessage response = await client.PostAsJsonAsync(AppUrlConstant.AddInternalusers, adduser);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    TempData["InternalUserAdd"] = "User Add Successfully";
                }
                ViewBag.ErrorMessage = "Data not  insert";
                return RedirectToAction("ShowInternalUsers");
            }
        }

        public async Task<IActionResult> UpdateInternalUsers(int UserID)
        {
            InternalUsers internalesers = null;
            using (HttpClient client = new HttpClient())
            {
                string url = $"{AppUrlConstant.GetInternalusersID}/{UserID}";
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    internalesers = JsonConvert.DeserializeObject<InternalUsers>(result);
                }
            }
            if (internalesers != null)
            {
       
                if (internalesers.Logo != null && internalesers.Logo.Length < 180_000) 
                {
                    string base64Logo = Convert.ToBase64String(internalesers.Logo);
                    TempData["LogoBase64"] = base64Logo;
                }
                else
                {
                    TempData["LogoBase64"] = null; 
                }

                TempData["EmplID"] = internalesers.EmpId;
                TempData["EmplName"] = $"{internalesers.FirstName} {internalesers.LastName}";
            }

            return View(internalesers);
        }



        [HttpPost]
        public async Task<IActionResult> EditInternalUsers(InternalUsers internalusers)
        {
            var file = Request.Form.Files.FirstOrDefault(f => f.Name == "Logo");

            if (file != null && file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    internalusers.Logo = ms.ToArray(); 
                }
            }
            using (HttpClient client = new HttpClient())
            {

                string Data = JsonConvert.SerializeObject(internalusers);
                StringContent content = new StringContent(Data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(AppUrlConstant.GetInternalusersID + "/" + internalusers.UserID, content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["IuserUpdate"] = "User Update";
                    return RedirectToAction("UpdateInternalUsers", new { UserID = internalusers.UserID });

                }
            }
            return View();
        }


        [HttpGet]
        public async  Task<IActionResult> OrganizationProfile(string legalEntityCode, string legalEntityName, string legalEntityId)
        {
            List<LegalEntity> legalEntities= new List<LegalEntity>();
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

                }
                
            }
            return View(legaldata);
        }


      

        public IActionResult UserContactDetails()
        {
            return View();
        }



    }
}
