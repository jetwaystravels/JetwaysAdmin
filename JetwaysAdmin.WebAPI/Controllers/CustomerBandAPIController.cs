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
    }
}
