using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JetwaysAdmin.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InternalUsersAPIController : ControllerBase
    {
        
        private readonly IInternalUsers<InternalUsers> _internaluser;
        public InternalUsersAPIController(
            IInternalUsers<InternalUsers> internaluser)
        {
            _internaluser = internaluser;
        }

        [HttpPost]
        [Route("AddInternalUsers")]
        public async Task<IActionResult> AddInternalUsers([FromBody] InternalUsers addinternalUsers)
        {
            if (addinternalUsers == null)
                return BadRequest("Invalid data.");
            if (string.IsNullOrWhiteSpace(addinternalUsers.Password))
                return BadRequest("Password is required.");
            addinternalUsers.Password = BCrypt.Net.BCrypt.HashPassword(addinternalUsers.Password);
            await _internaluser.AddInternalusers(addinternalUsers);
            return Ok(new { message = "User added successfully!" });
        }


        [HttpGet]
        [Route("GetInternalUsers")]
        public async Task<ActionResult<IEnumerable<InternalUsers>>> GetInternalUsers()
        {
            var getManageUser = await _internaluser.GetInternalUsers();
            return Ok(getManageUser);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<InternalUsers>> GetInternalUsersById(int id)
        {
            var internaluser = await _internaluser.GetInternalUsersById(id);
            if (internaluser == null)
            {
                return NotFound();
            }
            return Ok(internaluser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInternalUsers(int id, [FromBody] InternalUsers internalusers)
        {
            if (id != internalusers.UserID)
                return BadRequest("User ID mismatch.");

            var existingUser = await _internaluser.GetInternalUsersById(id);
            if (existingUser == null)
                return NotFound();

            existingUser.FirstName = internalusers.FirstName ?? existingUser.FirstName;
            existingUser.LastName = internalusers.LastName ?? existingUser.LastName;
            existingUser.MobileNumber = internalusers.MobileNumber ?? existingUser.MobileNumber;
            existingUser.BusinessEmail = internalusers.BusinessEmail ?? existingUser.BusinessEmail;
            existingUser.Logo = internalusers.Logo ?? existingUser.Logo;

            
            if (!string.IsNullOrWhiteSpace(internalusers.Password))
            {
                existingUser.Password = BCrypt.Net.BCrypt.HashPassword(internalusers.Password);
              
            }

            await _internaluser.UpdateInternalUsersById(existingUser);

            return Ok(new { message = "User updated successfully!" });
        }


    }
}
