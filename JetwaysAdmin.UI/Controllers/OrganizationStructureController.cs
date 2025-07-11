using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Migrations;
using JetwaysAdmin.UI.ApplicationUrl;
using JetwaysAdmin.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JetwaysAdmin.UI.Controllers
{
    public class OrganizationStructureController : Controller
    {
        public async Task<IActionResult> ShowOrganization(int Id, string legalEntityName, string legalEntityCode)
        {
           
            using (HttpClient client = new HttpClient())
            {
                string apiUrl = $"{AppUrlConstant.LegalHeirachy}?LegalEntityCode={legalEntityCode}";
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string jsonData = await response.Content.ReadAsStringAsync();
                    var flatList = JsonConvert.DeserializeObject<List<HierarchyLegalEntityView>>(jsonData);
                    var hierarchy = flatList
                        .Where(e => string.IsNullOrEmpty(e.ParentLegalEntityCode))
                        .Select(parent =>
                        {
                            parent.SubEntities = flatList
                                .Where(child => child.ParentLegalEntityCode == parent.LegalEntityCode)
                                .ToList();
                            return parent;
                        })
                        .ToList();
                    ViewBag.LegalEntityId = Id;
                    ViewBag.LegalEntityName = legalEntityName;
                    ViewBag.LegalEntityCode = legalEntityCode;
                    return View(hierarchy);
                }

                TempData["ErrorMessage"] = "Failed to retrieve data.";
                return View(new List<HierarchyLegalEntityView>());
            }
        }

    }
}