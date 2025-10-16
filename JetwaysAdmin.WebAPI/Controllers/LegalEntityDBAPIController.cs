using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JetwaysAdmin.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LegalEntityDBAPIController : ControllerBase
    {
        private readonly ILegalEntityDB<LegalEntityDB> _legalEntity;
        public LegalEntityDBAPIController(ILegalEntityDB<LegalEntityDB> legalEntity)
        {
            this._legalEntity = legalEntity;
        }
        [HttpPost]
        [Route("AddLegalEntityDB")]
        public async Task<ActionResult> AddLegalEntityUpload([FromBody] LegalEntityDB legalEntity)
        {
            if (legalEntity == null)
            {
                return BadRequest("Invalid data.");
            }
            await _legalEntity.AddLegalEntityDB(legalEntity);
            return Ok(new { message = "Customer added successfully!" });
        }
    }
}
