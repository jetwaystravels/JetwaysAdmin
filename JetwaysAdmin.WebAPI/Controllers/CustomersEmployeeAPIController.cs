using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JetwaysAdmin.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersEmployeeAPIController : ControllerBase
    {
        private readonly ICustomersEmployee<CustomersEmployee> _cutomerempl;
        public CustomersEmployeeAPIController(ICustomersEmployee<CustomersEmployee> cutomerempl)
        {
            this._cutomerempl = cutomerempl;
        }

        [HttpGet]
        [Route("GetCustomerEmployee")]
       
        public async Task<ActionResult<IEnumerable<CustomersEmployee>>> GetCustomerEmployee([FromQuery] string LegalEntityCode)
        {
            if (string.IsNullOrEmpty(LegalEntityCode))
            {
                return BadRequest("LegalEntityCode is required.");
            }

            var customerEmployees = await _cutomerempl.GetCustomerEmployeeByLegalEntity(LegalEntityCode);

            if (!customerEmployees.Any())
            {
                return NotFound("No employees found for the provided LegalEntityCode.");
            }

            return Ok(customerEmployees);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<CustomersEmployee>> GetUsersById(int id)
        {
            var user = await _cutomerempl.GetUsersById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }


        [HttpPost]
        [Route("AddUsers")]
        public async Task<ActionResult> AddUsers([FromBody] CustomersEmployee addUsers)
        {
            if (addUsers == null)
            {

                return BadRequest("Invalid data.");
            }

            await _cutomerempl.Addusers(addUsers);
            return Ok(new { message = "User added successfully!" });
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateInternalUsers(int id, CustomersEmployee internalusers)
        {
            if (id != internalusers.UserID)
            {
                return BadRequest("Customer ID mismatch.");
            }

            var suserupdate = await _cutomerempl.GetUsersById(id);
            if (suserupdate == null)
            {
                return NotFound();
            }
            suserupdate.FirstName = internalusers.FirstName ?? suserupdate.FirstName;
            suserupdate.LastName = internalusers.LastName ?? suserupdate.LastName;
            suserupdate.MobileNumber = internalusers.MobileNumber ?? suserupdate.MobileNumber;
            suserupdate.BusinessEmail = internalusers.BusinessEmail ?? suserupdate.BusinessEmail;
            suserupdate.Logo = internalusers.Logo ?? suserupdate.Logo;
            await _cutomerempl.UpdateUsersById(suserupdate);
            return Ok(new { message = "Customer updated successfully!" });
        }


    }
}
