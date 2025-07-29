using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

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


        [HttpGet]
        [Route("GetAllLegalEntity")]
        public async Task<ActionResult<IEnumerable<LegalEntity>>> GetAllLegalEntity()
        {
            var legalEntities = await _legalEntity.GetAllLegalEntity();
            var count = legalEntities.Count();
            var response = new
            {
                TotalCount = count,
                Data = legalEntities
            };

            return Ok(response);
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
            var entity = await _legalEntity.GetLegalEntityById(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
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
            //legalEntityupdate.LegalEntityCode = legalEntity.LegalEntityCode;
            legalEntityupdate.LegalEntityName = legalEntity.LegalEntityName ?? legalEntityupdate.LegalEntityName;
            legalEntityupdate.LegalEntityCode = legalEntity.LegalEntityCode ?? legalEntityupdate.LegalEntityCode;
            legalEntityupdate.AddressLine1 = legalEntity.AddressLine1 ?? legalEntityupdate.AddressLine1;
            legalEntityupdate.AddressLine2 = legalEntity.AddressLine2 ?? legalEntityupdate.AddressLine2;
            legalEntityupdate.City = legalEntity.City ?? legalEntityupdate.City;
            legalEntityupdate.State = legalEntity.State ?? legalEntityupdate.State;
            legalEntityupdate.Country = legalEntity.Country ?? legalEntityupdate.Country;
            legalEntityupdate.PostalCode = legalEntity.PostalCode ?? legalEntityupdate.PostalCode;
            legalEntityupdate.IntegrationRefNumber = legalEntity.IntegrationRefNumber ?? legalEntityupdate.IntegrationRefNumber;
            legalEntityupdate.CustomerBaseCurrency = legalEntity.CustomerBaseCurrency ?? legalEntityupdate.CustomerBaseCurrency;
            legalEntityupdate.CustomerBaseCountry = legalEntity.CustomerBaseCountry ?? legalEntityupdate.CustomerBaseCountry;
            legalEntityupdate.AcountActivationDate = legalEntity.AcountActivationDate ?? legalEntityupdate.AcountActivationDate;
            legalEntityupdate.AccountDeactivationDate = legalEntity.AccountDeactivationDate ?? legalEntityupdate.AccountDeactivationDate;
            legalEntityupdate.AppStatus = legalEntity.AppStatus ?? legalEntityupdate.AppStatus;


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
