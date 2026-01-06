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
        //public async Task<ActionResult<IEnumerable<CustomerDesignation>>> GetAllCustomerDepartment([FromQuery] string LegalEntityCode)
        public async Task<ActionResult<IEnumerable<CustomerDesignation>>> GetAllCustomerDepartment()
        {
            //if (string.IsNullOrEmpty(LegalEntityCode))
            //{
            //    return BadRequest("LegalEntityCode is required.");
            //}
            //var legalEntities = await _designation.GetAllCustomerDesignation(LegalEntityCode);
            var legalEntities = await _designation.GetAllCustomerDesignation();
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
        [HttpGet("{DesignationID}")]
        public async Task<ActionResult<CustomerDesignation>> GetCustomerDesignationById(int DesignationID)
        {
            var entity = await _designation.GetCustomerDesignationById(DesignationID);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }
        [HttpPut("{DesignationID}")]
        public async Task<ActionResult> UpdateCustomerDesignation(int DesignationID, CustomerDesignation designation)
        {
            if (DesignationID != designation.DesignationID)
            {
                return BadRequest("Customer ID mismatch.");
            }

            var DesignationUpdate = await _designation.GetCustomerDesignationById(DesignationID);
            if (DesignationUpdate == null)
            {
                return NotFound();
            }
           
            DesignationUpdate.DesignationName = designation.DesignationName ?? DesignationUpdate.DesignationName;
            DesignationUpdate.DesignationCode = designation.DesignationCode ?? DesignationUpdate.DesignationCode;

            await _designation.UpdateDesignationData(DesignationUpdate);
            return Ok(new { message = "Designation updated successfully!" });
        }

    }
}
