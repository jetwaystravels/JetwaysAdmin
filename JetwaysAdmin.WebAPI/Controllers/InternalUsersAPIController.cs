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

        public InternalUsersAPIController(IInternalUsers<InternalUsers> internaluser)
        {
            _internaluser=internaluser;
        }

        [HttpPost]
        [Route("AddInternalUsers")]
        public async Task<ActionResult> AddInternalUsers([FromBody] InternalUsers addinternalUsers)
        {
            if (addinternalUsers == null)
            {

                return BadRequest("Invalid data.");
            }

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
        public async Task<ActionResult> UpdateInternalUsers(int id, InternalUsers internalusers)
        {
            if (id != internalusers.UserID)
            {
                return BadRequest("Customer ID mismatch.");
            }

            var suserupdate = await _internaluser.GetInternalUsersById(id);
            if (suserupdate == null)
            {
                return NotFound();
            }
            suserupdate.FirstName = internalusers.FirstName ?? suserupdate.FirstName;
            suserupdate.LastName = internalusers.LastName ?? suserupdate.LastName;
            suserupdate.MobileNumber = internalusers.MobileNumber ?? suserupdate.MobileNumber;
            suserupdate.BusinessEmail = internalusers.BusinessEmail ?? suserupdate.BusinessEmail;
            suserupdate.Logo = internalusers.Logo ?? suserupdate.Logo;
            await _internaluser.UpdateInternalUsersById(suserupdate);
            return Ok(new { message = "Customer updated successfully!" });
        }


    }
}
