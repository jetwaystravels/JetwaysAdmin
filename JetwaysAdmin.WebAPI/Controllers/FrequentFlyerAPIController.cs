using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories;
using JetwaysAdmin.Repositories.Interface;
using JetwaysAdmin.Repositories.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JetwaysAdmin.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FrequentFlyerAPIController : ControllerBase
    {
        private readonly IFrequentFlyer<EmployeeFrequentFlyer> _frequentFlyer;
        public FrequentFlyerAPIController(IFrequentFlyer<EmployeeFrequentFlyer> frequentFlyer)
        {
            this._frequentFlyer = frequentFlyer;
        }


        [HttpPost]
        [Route("AddFrequentFlyer")]
        public async Task<ActionResult> AddFrequentFlyer([FromBody] EmployeeFrequentFlyer employeefrequentFlyer)
        {
            if (employeefrequentFlyer == null)
            {
                return BadRequest("Invalid data.");
            }
            await _frequentFlyer.AddFrequentFlyer(employeefrequentFlyer);
            return Ok(new { message = "Customer added successfully!" });
        }


        [HttpGet]
        [Route("GetFrequentFlyer")]
        public async Task<ActionResult<IEnumerable<EmployeeFrequentFlyer>>> GetAllFrequentFlyer()
        {
            var frequentFlyer = await _frequentFlyer.GetAllFrequentFlyer();
            return Ok(frequentFlyer);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeFrequentFlyer>> GetFrequentFlyerById(int id)
        {
            var flyer = await _frequentFlyer.GetFrequentFlyerById(id);
            if (flyer == null)
            {
                return NotFound();
            }
            return Ok(flyer);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateLegalEntity(int id, EmployeeFrequentFlyer frequentFlyer)
        {
            if (id != frequentFlyer.FrequentFlyerID)
            {
                return BadRequest("Customer ID mismatch.");
            }

            var frequentFlyerupdate = await _frequentFlyer.GetFrequentFlyerById(id);
            if (frequentFlyerupdate == null)
            {
                return NotFound();
            }
            //legalEntityupdate.LegalEntityCode = legalEntity.LegalEntityCode;
            frequentFlyerupdate.FrequentFlyerNumber = frequentFlyer.FrequentFlyerNumber ?? frequentFlyerupdate.FrequentFlyerNumber;
     


            await _frequentFlyer.UpdateFrequentFlyer(frequentFlyerupdate);
            return Ok(new { message = "Customer updated successfully!" });
        }

    }
}
