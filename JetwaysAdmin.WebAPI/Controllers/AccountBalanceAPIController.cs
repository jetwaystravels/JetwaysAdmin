using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JetwaysAdmin.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountBalanceAPIController : ControllerBase
    {
        private readonly ICustomerAccountBalance<CustomerAccountBalance> _accountbalance;
        public AccountBalanceAPIController(ICustomerAccountBalance<CustomerAccountBalance> accountbalance)
        {
            this._accountbalance = accountbalance;
        }


        [HttpPost]
        [Route("AccountBalance")]
        public async Task<ActionResult> AccountBalance([FromBody]  CustomerAccountBalance accountBalance)
        {
            if (accountBalance == null) {
                return BadRequest("Invalid data.");
            }
            await _accountbalance.AccountBalance(accountBalance);
            return Ok(new { message = "Customer added successfully!" });

        }
    }
}
