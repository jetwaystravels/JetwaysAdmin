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
        public async Task<ActionResult<IEnumerable<LegalEntity>>> GetCustomerEmployee()
        {
            var cutomeremplo = await _cutomerempl.GetAllCustomerEmployee();
            return Ok(cutomeremplo);
        }
    }
}
