using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JetwaysAdmin.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeBillingEntityAPIController : ControllerBase
    {
       private readonly IEmployeeBillingEntity<EmployeeBillingEntity> _emplyebilling;

        public EmployeeBillingEntityAPIController(IEmployeeBillingEntity<EmployeeBillingEntity> empbillingl)
        {
            this._emplyebilling = empbillingl;
        }

        //Billing Entity Customer 
        [HttpPost]
        [Route("AddBillingEntity")]
        public async Task<ActionResult> AddBillingEntity([FromBody] EmployeeBillingEntity empBillingl)
        {
            if (empBillingl == null)
            {

                return BadRequest("Invalid data.");
            }
            var existingEntity = await _emplyebilling.GetEmplBillingEntityById(empBillingl.UserID);
            if (existingEntity != null)
            {
                return Conflict(new { message = "A billing entity for this user already exists." });
            }
            await _emplyebilling.AddEmplBillingEntity(empBillingl);
            return Ok(new { message = "User added successfully!" });
        }
    }
}
