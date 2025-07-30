using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JetwaysAdmin.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersCredentialAPIController : ControllerBase
    {
        private readonly ISuppliersCredential<SuppliersCredential> _suppliersCredentialService;

        public SuppliersCredentialAPIController(ISuppliersCredential<SuppliersCredential> suppliersCredentialService)
        {
            _suppliersCredentialService = suppliersCredentialService;
        }
        [HttpPost("addsuppliercred")]
        public async Task<IActionResult> AddSupplierCredential([FromBody] SuppliersCredential supplierscredential)
        {
            if (supplierscredential == null)
            {
                return BadRequest("Invalid supplier credential data.");
            }

            await _suppliersCredentialService.AddSupplierCredential(supplierscredential);
            return Ok("Supplier credential added successfully.");
        }
        [HttpGet("getsuppliercred")]
        public async Task<IActionResult> GetSupplierCredentials()
        {
            var credentials = await _suppliersCredentialService.GetSupplierCredential();
            return Ok(credentials);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SuppliersCredential>> GetSupplierCredentialsById(int Id)
        {
            var updatesupplier = await _suppliersCredentialService.GetSupplierCredentialById(Id);
            if (updatesupplier == null)
            {
                return NotFound();
            }
            return Ok(updatesupplier);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSuppliersCredential(int Id, SuppliersCredential supplierscredential)
        {
            if (Id != supplierscredential.Id)
            {
                return BadRequest("Customer ID mismatch.");
            }
            var ssupplierupdate = await _suppliersCredentialService.GetSupplierCredentialById(Id);
            if (ssupplierupdate == null)
            {
                return NotFound();
            }
            ssupplierupdate.SupplierId = supplierscredential.SupplierId ?? ssupplierupdate.SupplierId;
            ssupplierupdate.AgentName = supplierscredential.AgentName ?? ssupplierupdate.AgentName;
            ssupplierupdate.UserName = supplierscredential.UserName ?? ssupplierupdate.UserName;
            ssupplierupdate.Password = supplierscredential.Password ?? ssupplierupdate.Password;
            ssupplierupdate.CredentialsName = supplierscredential.CredentialsName ?? ssupplierupdate.CredentialsName;
            ssupplierupdate.IATAGroup = supplierscredential.IATAGroup ?? ssupplierupdate.IATAGroup;
            ssupplierupdate.AssociatedFareTypes = supplierscredential.AssociatedFareTypes ?? ssupplierupdate.AssociatedFareTypes;
            ssupplierupdate.TravelType = supplierscredential.TravelType ?? ssupplierupdate.TravelType;
            ssupplierupdate.ClientId = supplierscredential.ClientId ?? ssupplierupdate.ClientId;
            ssupplierupdate.OrganizationId = supplierscredential.OrganizationId ?? ssupplierupdate.OrganizationId;
            ssupplierupdate.Status = supplierscredential.Status ?? ssupplierupdate.Status;
            await _suppliersCredentialService.UpdateSupplierCredentialById(ssupplierupdate);
            return Ok(new { message = "Customer updated successfully!" });
        }
    }
}
