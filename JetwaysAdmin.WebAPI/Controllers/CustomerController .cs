using JetwaysAdmin.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using JetwaysAdmin.WebAPI.Models;
using JetwaysAdmin.Entity;


namespace JetwaysAdmin.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {

        private readonly ICustomer<Customer> _Customer;
        private readonly ICustomerDetailsByEmail<CustomerDetails> _CustomerDetailsByEmail;
        public CustomerController(ICustomer<Customer> Customer, ICustomerDetailsByEmail<CustomerDetails> customerDetailsByEmail)
        {
            this._Customer = Customer;
            _CustomerDetailsByEmail = customerDetailsByEmail;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
        {
            return Ok(await _Customer.GetAllCustomers());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerById(int id)
        {
            var customer = await _Customer.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }


        [HttpGet]
        [Route("GetCustomerDetailsByEmail")]
        public async Task<ActionResult<CustomerDetails>> GetCustomerDetailsByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest("Email is required.");

            var result = await _CustomerDetailsByEmail.GetCustomerDetailsByEmailAsync(email);
            return Ok(result);
        }

        [HttpPost]
        
        public async Task<ActionResult> AddCustomer( Customer customer)
        {
            if (customer == null)
            {
                return BadRequest("Invalid data.");
            }

            await _Customer.AddCustomer(customer);
            return Ok(new { message = "Customer added successfully!" });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCustomer(int id, Customer customer)
        {
            if (id != customer.CustomerID)
            {
                return BadRequest("Customer ID mismatch.");
            }

            var existingCustomer = await _Customer.GetCustomerById(id);
            if (existingCustomer == null)
            {
                return NotFound();
            }

            await _Customer.UpdateCustomer(customer);
            return Ok(new { message = "Customer updated successfully!" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            var customer = await _Customer.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }

            await _Customer.DeleteCustomer(id);
            return Ok(new { message = "Customer deleted successfully!" });
        }
    }
}
