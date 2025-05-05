using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JetwaysAdmin.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountDetailsAPIController : ControllerBase
    { 
         
        private readonly IAccountDetails<AccountDetails> _accountdetails;
        public AccountDetailsAPIController(IAccountDetails<AccountDetails> accountdetails)
        {
            this._accountdetails = accountdetails;
        }


    
        [HttpPost]
        [Route("AddAccountDetails")]
        public async Task<ActionResult> AddAccountDetails([FromBody] AccountDetails accountdetails)
        {
            if (accountdetails == null)
            {
                return BadRequest("Invalid data.");
            }

            await _accountdetails.AddAccountDetails(accountdetails);
            return Ok(new { message = "Customer added successfully!" });
        }
    }
}
