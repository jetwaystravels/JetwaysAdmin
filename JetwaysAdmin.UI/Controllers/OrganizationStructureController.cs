using JetwaysAdmin.Entity;
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
            TempData["LegalEntityName1"] = legalEntityName;
            TempData["LegalEntityCode1"] = legalEntityCode;
            using (HttpClient client = new HttpClient())
            {
                string apiUrl = $"{AppUrlConstant.LegalHeirachy}?LegalEntityCode={legalEntityCode}";
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonData = await response.Content.ReadAsStringAsync();
                    var flatList = JsonConvert.DeserializeObject<List<HierarchyLegalEntityView>>(jsonData);

                    // Build parent-child hierarchy
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

                    return View(hierarchy); // Pass only parent-level list with nested SubEntities
                }

                TempData["ErrorMessage"] = "Failed to retrieve data.";
                return View(new List<HierarchyLegalEntityView>());
            }
        }

    }
}