using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Implementations;
using JetwaysAdmin.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JetwaysAdmin.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerDepartmentAPIController : ControllerBase
    {
        private readonly ICustomerDepartment<CustomerDepartmentData> _department;
        public CustomerDepartmentAPIController(ICustomerDepartment<CustomerDepartmentData> department)
        {
            this._department = department;
        }

        [HttpGet]
        [Route("GetAllCustomerDepartment")]
        public async Task<ActionResult<IEnumerable<CustomerDepartmentData>>> GetAllCustomerDepartment()
        {
           
            var legalEntities = await _department.GetAllCustomerDepartment();
            return Ok(legalEntities);
        }

        [HttpPost]
        [Route("AddCustomerDepartment")]
        public async Task<ActionResult> AddCustomerDepartment([FromBody] CustomerDepartmentData customerdepartment)
        {
            if (customerdepartment == null)
            {
                return BadRequest("Invalid data.");
            }
            await _department.AddCustomerDepartment(customerdepartment);
            return Ok(new { message = "Customer department added successfully!" });
        }
        [HttpGet("{DepartmentID}")]
        public async Task<ActionResult<CustomerDepartmentData>> GetCustomerDepartmentById(int DepartmentID)
        {
            var entity = await _department.GetCustomerDepartmentById(DepartmentID);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPut("{DepartmentID}")]
        public async Task<ActionResult> UpdateCustomerDepartment(int DepartmentID, CustomerDepartmentData department)
        {
            if (DepartmentID != department.DepartmentID)
            {
                return BadRequest("Customer ID mismatch.");
            }

            var DepartmentUpdate = await _department.GetCustomerDepartmentById(DepartmentID);
            if (DepartmentUpdate == null)
            {
                return NotFound();
            }
            //legalEntityupdate.LegalEntityCode = legalEntity.LegalEntityCode;
            DepartmentUpdate.DepartmentName = department.DepartmentName ?? DepartmentUpdate.DepartmentName;
            DepartmentUpdate.DepartmentCode = department.DepartmentCode ?? DepartmentUpdate.DepartmentCode;

            await _department.UpdateDepartmentData(DepartmentUpdate);
            return Ok(new { message = "Customer updated successfully!" });
        }
    }
}
