using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JetwaysAdmin.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageStaffAPIController : ControllerBase
    {
        private readonly IManageStaff<CustomerManageStaff> _managestaff;

        public ManageStaffAPIController(IManageStaff<CustomerManageStaff> managestaff)
        {
            this._managestaff = managestaff;
        }



        [HttpPost]
        [Route("ManageStaff")]
        public async Task<IActionResult> ManageStaff([FromBody] CustomerManageStaff customerManageStaff)
        {
            if (customerManageStaff == null) {
                return BadRequest("Invalid Data..");
            
            }

            await _managestaff.ManageStaff(customerManageStaff);
            return Ok(new { message = "Manage Staff successfully!", data = customerManageStaff });

        }
    }
}
