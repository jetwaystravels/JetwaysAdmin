using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace JetwaysAdmin.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LegalEntityAPIController : Controller
    {
        private readonly ILegalEntity<LegalEntity> _legalEntity;
        public LegalEntityAPIController(ILegalEntity<LegalEntity> legalEntity)
        {
            this._legalEntity = legalEntity;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}


        [HttpGet]
        [Route("GetAllLegalEntity")]
        public async Task<ActionResult<IEnumerable<LegalEntity>>> GetAllLegalEntity()
        {
            return Ok(await _legalEntity.GetAllLegalEntity());
        }


        [HttpPost]
        [Route("AddLegalEntity")]
        public async Task<ActionResult> AddLegalEntity([FromBody] LegalEntity legalEntity)
        {
            if (legalEntity == null)
            {
                return BadRequest("Invalid data.");
            }

            await _legalEntity.AddLegalEntity(legalEntity);
            return Ok(new { message = "Customer added successfully!" });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LegalEntity>> GetLegalEntityById(int id)
        {
            var LegalEntity = await _legalEntity.GetLegalEntityById(id);
            if (LegalEntity == null)
            {
                return NotFound();
            }
            return Ok(LegalEntity);
        }


        

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateLegalEntity(int id, LegalEntity legalEntity)
        {
            if (id != legalEntity.Id)
            {
                return BadRequest("Customer ID mismatch.");
            }

            var legalEntityupdate = await _legalEntity.GetLegalEntityById(id);
            if (legalEntityupdate == null)
            {
                return NotFound();
            }

            await _legalEntity.UpdateLegalEntity(legalEntityupdate);
            return Ok(new { message = "Customer updated successfully!" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLegalEntity(int id)
        {
            var customerdelete = await _legalEntity.GetLegalEntityById(id);
            if (customerdelete == null)
            {
                return NotFound();
            }

            await _legalEntity.DeleteLegalEntity(id);
            return Ok(new { message = "Customer deleted successfully!" });
        }
    }
}
