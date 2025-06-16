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
    }
}
