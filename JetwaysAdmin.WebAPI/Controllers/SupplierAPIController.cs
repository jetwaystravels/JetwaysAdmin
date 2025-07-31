using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using JetwaysAdmin.Repositories.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JetwaysAdmin.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierAPIController : ControllerBase
    {
        private readonly IAddNewSupplier<AddSupplier> _supplier;
      //  private readonly IAddNewSupplier<legalentitySupplier> _supplierRepository;

        public SupplierAPIController(IAddNewSupplier<AddSupplier> supplier)
        {
            this._supplier = supplier;
           
        }

        [HttpPost]
        [Route("AddSupplier")]
        public async Task<ActionResult> AddSupplier([FromBody] AddSupplier addSupllier)
        {
            if (addSupllier == null)
            {

                return BadRequest("Invalid data.");
            }

            await _supplier.AddNewSupplier(addSupllier);
            return Ok(new { message = "User added successfully!" });
        }


        [HttpGet]
        [Route("GetSupplier")]
        public async Task<ActionResult<IEnumerable<AddSupplier>>> GetSupplier()
        {
            var getManageUser = await _supplier.GetSupplier();
            return Ok(getManageUser);
        }
        // GET api/suppliers/{legalEntityCode}
        [HttpGet("legalentitysuppliers")]
        
        public async Task<IActionResult> Getlegalentitysuppliers(string legalEntityCode)
        {
            if (string.IsNullOrEmpty(legalEntityCode))
                return BadRequest("LegalEntityCode is required.");

            var suppliers = await _supplier.GetSuppliersByLegalEntityAsync(legalEntityCode);

            return Ok(suppliers);
        }

        [HttpPost("Updatelegalentitysuppliers")]
        public async Task<IActionResult> AddOrUpdate([FromBody] LegalEntitySupplierDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data.");

            await _supplier.AddOrUpdateLegalEntitySupplierStatusAsync(dto.LegalEntityCode, dto.SupplierID, dto.IsActive);

            return Ok(new { message = "Record processed successfully." });
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<AddSupplier>> GetSupplierById(int id)
        {
            var supplier = await _supplier.GetSupplierById(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return Ok(supplier);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSupplier(int id, AddSupplier supplier)
        {
            if (id != supplier.SupplierId)
            {
                return BadRequest("Customer ID mismatch.");
            }

            var supplierupdate = await _supplier.GetSupplierById(id);
            if (supplierupdate == null)
            {
                return NotFound();
            }
          
            supplierupdate.SupplierName = supplier.SupplierName ?? supplierupdate.SupplierName;
            supplierupdate.SupplierEmails = supplier.SupplierEmails ?? supplierupdate.SupplierEmails;
            supplierupdate.SupplierCode = supplier.SupplierCode ?? supplierupdate.SupplierCode;
            supplierupdate.AddressLine1 = supplier.AddressLine1 ?? supplierupdate.AddressLine1;
            supplierupdate.AddressLine2 = supplier.AddressLine2 ?? supplierupdate.AddressLine2;
            supplierupdate.City = supplier.City ?? supplierupdate.City;
            supplierupdate.Country = supplier.Country ?? supplierupdate.Country;
            supplierupdate.PhoneNumber = supplier.PhoneNumber ?? supplierupdate.PhoneNumber;
            supplierupdate.State = supplier.State ?? supplierupdate.State;
            supplierupdate.SendAmendmentNotifications = supplier.SendAmendmentNotifications ?? supplierupdate.SendAmendmentNotifications;
            supplierupdate.AppStatus = supplier.AppStatus ?? supplierupdate.AppStatus;
            supplierupdate.Logo = supplier.Logo ?? supplierupdate.Logo;
             await _supplier.UpdateSupplierById(supplierupdate);
            return Ok(new { message = "Customer updated successfully!" });
        }




    }
}
