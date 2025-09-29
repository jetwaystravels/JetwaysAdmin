using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis.Scripting;


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
        [Route("GetCustomerEmployeeAll")]
        public async Task<ActionResult<IEnumerable<InternalUsers>>> GetInternalUsers()
        {
            var getManageUser = await _cutomerempl.GetAllCustomerEmployee();
            return Ok(getManageUser);
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
            suserupdate.DateOfBirth = internalusers.DateOfBirth ?? suserupdate.DateOfBirth;
            suserupdate.Designation = internalusers.Designation ?? suserupdate.Designation;
            suserupdate.Department = internalusers.Department ?? suserupdate.Department;
            suserupdate.Bands = internalusers.Bands ?? suserupdate.Bands;
            suserupdate.ReportingManager = internalusers.ReportingManager ?? suserupdate.ReportingManager;
            suserupdate.Logo = internalusers.Logo ?? suserupdate.Logo;
            await _cutomerempl.UpdateUsersById(suserupdate);
            return Ok(new { message = "Customer updated successfully!" });
        }

        [HttpPost]
        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] Entity.ResetPasswordRequest request)
        {
            if (request == null || request.UserId <= 0 || string.IsNullOrWhiteSpace(request.Password))
                return BadRequest("Invalid request.");

            // TODO: enforce your password policy here (min length, complexity, etc.)
            // Example:
            if (request.Password.Length < 8)
                return BadRequest("Password must be at least 8 characters.");

            // 🔐 Hash the password (BCrypt recommended). Install BCrypt.Net-Next if needed.
            // dotnet add package BCrypt.Net-Next
            //var hashed = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var updated = await _cutomerempl.UpdatePasswordAsync(request.UserId, request.Password);
            if (!updated) return NotFound("User not found.");

            return Ok(new { message = "Password updated successfully." });
        }

        
        [HttpPost("updatestatus")]
        public async Task<IActionResult> UpdateStatus(int id, int status, string legalEntityCode)
        {
            var success = await _cutomerempl.UpdateStatusAsync(id, status, legalEntityCode);

            if (!success)
                return BadRequest(new { success = false, message = "User not found or update failed." });

            return Ok(new { success = true, newStatus = status });
        }




    }
}
