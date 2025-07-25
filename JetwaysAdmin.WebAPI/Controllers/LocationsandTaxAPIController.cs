using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JetwaysAdmin.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsandTaxAPIController : ControllerBase
    {
        private readonly ILocationsandTax<LocationsandTax> _locationsandtax;
        public LocationsandTaxAPIController(ILocationsandTax<LocationsandTax> locationsandtax)
        {
            this._locationsandtax = locationsandtax;
        }




        [HttpPost]
        [Route("AddLocationsandTax")]
        public async Task<ActionResult> AddLocationsandTax([FromBody] LocationsandTax locationsandtax)
        {
            if (locationsandtax == null)
            {
                return BadRequest("Invalid data.");
            }
            await _locationsandtax.AddLocationTax(locationsandtax);
            return Ok(new { message = "LocationsandTax added successfully!" });
        }

        [HttpGet]
        [Route("GetLocationsandTax")]
        public async Task<ActionResult<IEnumerable<LocationsandTax>>> GetLocationsandTax([FromQuery] string LegalEntityCode)
        {
            if (string.IsNullOrEmpty(LegalEntityCode))
            {
                return BadRequest("LegalEntityCode is required.");
            }

            var customerLocationsandTax = await _locationsandtax.GetLocationsandTaxByLegalEntity(LegalEntityCode);

            if (!customerLocationsandTax.Any())
            {
                return NotFound("No employees found for the provided LegalEntityCode.");
            }

            return Ok(customerLocationsandTax);
        }
    }
}
