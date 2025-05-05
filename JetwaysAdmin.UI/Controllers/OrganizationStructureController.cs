using JetwaysAdmin.Entity;
using Microsoft.AspNetCore.Mvc;

namespace JetwaysAdmin.UI.Controllers
{
    public class OrganizationStructureController : Controller
    {
        public IActionResult ShowOrganization(int Id,string legalEntityName, string legalEntityCode)
        {
            var allEntities = new List<LegalEntity>
                {
                new LegalEntity { Id= Id, LegalEntityName = legalEntityName, LegalEntityCode = legalEntityCode }
                
                };
            var allEntitiesView = allEntities
        .Where(x => x.Id==Id && x.LegalEntityName == legalEntityName && x.LegalEntityCode == legalEntityCode)
        .ToList();

            return View(allEntitiesView);
        }
    }
}
