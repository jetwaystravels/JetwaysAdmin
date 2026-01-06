using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JetwaysAdmin.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerBandAPIController : ControllerBase
    {
        private readonly ICustomerBand<CustomerBand> _bandData;

        public CustomerBandAPIController(ICustomerBand<CustomerBand> internaluser)
        {
            _bandData = internaluser;
        }

        [HttpGet]
        [Route("GetCustomerBand")]
        public async Task<ActionResult<IEnumerable<CustomerBand>>> GetCustomerBand()
        {
           

           var getBAnd = await _bandData.GetAllBand();
            return Ok(getBAnd);
        }
        [HttpPost]
        [Route("AddCustomerBand")]
        public async Task<ActionResult> AddCustomerBand([FromBody] CustomerBand customerbanddata)
        {
            if (customerbanddata == null)
            {
                return BadRequest("Invalid data.");
            }
            await _bandData.AddCustomerBand(customerbanddata);
            return Ok(new { message = "Customer department added successfully!" });
        }
        [HttpGet("{BandID}")]
        public async Task<ActionResult<CustomerBand>> GetCustomerBandById(int BandID)
        {
            var entity = await _bandData.GetCustomerBandById(BandID);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }
        [HttpPut("{BandID}")]
        public async Task<ActionResult> UpdateCustomerBand(int BandID, CustomerBand band)
        {
            if (BandID != band.BandID)
            {
                return BadRequest("Band ID mismatch.");
            }
            var BandUpdate = await _bandData.GetCustomerBandById(BandID);
            if (BandUpdate == null)
            {
                return NotFound();
            }
            BandUpdate.BandName = band.BandName ?? BandUpdate.BandName;
            BandUpdate.BandCode = band.BandCode ?? BandUpdate.BandCode;
            BandUpdate.BandLevel = band.BandLevel ?? BandUpdate.BandLevel;
            await _bandData.UpdateBandData(BandUpdate);
            return Ok(new { message = "Band updated successfully!" });
        }
    }
}
