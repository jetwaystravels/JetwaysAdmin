using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JetwaysAdmin.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerDesignationAPIController : ControllerBase
    {
        private readonly ICustomerDesignation<CustomerDesignation> _designation;
        public CustomerDesignationAPIController(ICustomerDesignation<CustomerDesignation> designation)
        {
            _designation = designation;
        }

        [HttpGet]
        [Route("GetAllCustomerDesignation")]
        public async Task<ActionResult<IEnumerable<CustomerDesignation>>> GetAllCustomerDepartment([FromQuery] string LegalEntityCode)
        {
            if (string.IsNullOrEmpty(LegalEntityCode))
            {
                return BadRequest("LegalEntityCode is required.");
            }
            var legalEntities = await _designation.GetAllCustomerDesignation(LegalEntityCode);
            return Ok(legalEntities);
        }

        [HttpPost]
        [Route("AddCustomerDesignation")]
        public async Task<ActionResult> AddCustomerDepartment([FromBody] CustomerDesignation customerdesignation)
        {
            if (customerdesignation == null)
            {
                return BadRequest("Invalid data.");
            }
            await _designation.AddCustomerDesignation(customerdesignation);
            return Ok(new { message = "Customer department added successfully!" });
        }
    }
}
