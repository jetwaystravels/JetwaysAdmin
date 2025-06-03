using JetwaysAdmin.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using JetwaysAdmin.WebAPI.Models;
using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Implementations;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace JetwaysAdmin.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {

        private readonly ICustomer<Customer> _Customer;
        private readonly ICustomerDetailsByEmail<CustomerDetails> _CustomerDetailsByEmail;
        private readonly IBillingEntity<BillingEntity> _BillingEntity;
        private readonly ICompanyEmployeeGST<CompanyEmployeeGSTDetails> _CompanyEmployeeGST;
        private readonly IHierarchyLegalEntity<HierarchyLegalEntity> _hierarchyLegalEntity;
        public CustomerController(ICustomer<Customer> Customer, ICustomerDetailsByEmail<CustomerDetails> customerDetailsByEmail, ICompanyEmployeeGST<CompanyEmployeeGSTDetails> companyEmployeeGST, IHierarchyLegalEntity<HierarchyLegalEntity> hierarchyLegalEntity, IBillingEntity<BillingEntity> billingEntity)
        {
            this._Customer = Customer;
            _CustomerDetailsByEmail = customerDetailsByEmail;
            _CompanyEmployeeGST = companyEmployeeGST;
            _hierarchyLegalEntity = hierarchyLegalEntity;
            _BillingEntity = billingEntity;
            //  _CompanyEmployeeGST = CompanyEmployeeGST;
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
        [HttpGet]
        [Route("count")]
       
        public async Task<IActionResult> GetCustomerCount()
        {
            var count = await _Customer.GetCustomerCount();
            return Ok(count);
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

        [HttpGet]
        [Route("GetBillingEntity")]
        public async Task<ActionResult<BillingEntity>> GetBillingEntity(string legalEntityCode ,string employeeCode )
        {
            if (string.IsNullOrEmpty(legalEntityCode) || string.IsNullOrEmpty(employeeCode))
            {
                return BadRequest("EmployeeCode and LegalEntityCode are required.");
            }

            var result = await _BillingEntity.GetBillingEntityAsync(legalEntityCode,employeeCode);
            return Ok(result);
        }


        [HttpGet]
        [Route("GetCompanyEmployeeGST")]
        public async Task<ActionResult<CompanyEmployeeGSTDetails>> GetCompanyEmployeeGst(string employeeCode, string legalEntityCode)
        {
            if (string.IsNullOrEmpty(employeeCode) || string.IsNullOrEmpty(legalEntityCode))
            {
                return BadRequest("EmployeeCode and LegalEntityCode are required.");
            }

            var result = await _CompanyEmployeeGST.GetCompanyEmployeeGstAsync(employeeCode, legalEntityCode);
            return Ok(result);
        }


        [HttpGet]
        [Route("Gethierarchicallegal")]
        public async Task<ActionResult<IEnumerable<HierarchyLegalEntity>>> GetHierarchicalData(string legalEntityCode)
        {
            var data = await _hierarchyLegalEntity.GetHierarchicallegalentityAsync(legalEntityCode);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
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
