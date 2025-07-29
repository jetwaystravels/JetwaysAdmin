using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using JetwaysAdmin.Repositories.Migrations;
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
        [HttpGet("{LocationID}")]
        public async Task<ActionResult<LocationsandTax>> GetLocationTaxById(int LocationID)
        {
            var locationtax = await _locationsandtax.GetLocationTaxById(LocationID);
            if (locationtax == null)
            {
                return NotFound();
            }
            return Ok(locationtax);
        }
        [HttpPut("{LocationID}")]
        public async Task<ActionResult> UpdateLocationTax(int locationId, LocationsandTax locationsandtax)
        {
            if (locationId != locationsandtax.LocationID)
            {
                return BadRequest("Customer ID mismatch.");
            }
            var locationtaxupdate = await _locationsandtax.GetLocationTaxById(locationId);
            if (locationtaxupdate == null)
            {
                return NotFound();
            }
            locationtaxupdate.LocationName = locationsandtax.LocationName ?? locationtaxupdate.LocationName;
            locationtaxupdate.LocationCode = locationsandtax.LocationCode ?? locationtaxupdate.LocationCode;
            locationtaxupdate.AddressLine1 = locationsandtax.AddressLine1 ?? locationtaxupdate.AddressLine1;
            locationtaxupdate.AddressLine2 = locationsandtax.AddressLine2 ?? locationtaxupdate.AddressLine2;
            locationtaxupdate.Country = locationsandtax.Country ?? locationtaxupdate.Country;
            locationtaxupdate.State = locationsandtax.State ?? locationtaxupdate.State;
            locationtaxupdate.City = locationsandtax.City ?? locationtaxupdate.City;
            locationtaxupdate.PostalCode = locationsandtax.PostalCode ?? locationtaxupdate.PostalCode;
            locationtaxupdate.Latitude = locationsandtax.Latitude ?? locationtaxupdate.Latitude;
            locationtaxupdate.Longitude = locationsandtax.Longitude ?? locationtaxupdate.Longitude;
            locationtaxupdate.GSTRegistered = locationsandtax.GSTRegistered ?? locationtaxupdate.GSTRegistered;
            locationtaxupdate.UINRegistered = locationsandtax.UINRegistered ?? locationtaxupdate.UINRegistered;
            locationtaxupdate.GSTNumber = locationsandtax.GSTNumber ?? locationtaxupdate.GSTNumber;
            locationtaxupdate.GSTName = locationsandtax.GSTName ?? locationtaxupdate.GSTName;
            locationtaxupdate.GSTEmail = locationsandtax.GSTEmail ?? locationtaxupdate.GSTEmail;
            locationtaxupdate.MobileCountryCode = locationsandtax.MobileCountryCode ?? locationtaxupdate.MobileCountryCode;
            locationtaxupdate.MobileNumber = locationsandtax.MobileNumber ?? locationtaxupdate.MobileNumber;
            locationtaxupdate.PersonalBooking = locationsandtax.PersonalBooking ?? locationtaxupdate.PersonalBooking;
            locationtaxupdate.IsSEZ = locationsandtax.IsSEZ ?? locationtaxupdate.IsSEZ;
            locationtaxupdate.LocationHead = locationsandtax.LocationHead ?? locationtaxupdate.LocationHead;
            locationtaxupdate.LocationManager1 = locationsandtax.LocationManager1 ?? locationtaxupdate.LocationManager1;
            locationtaxupdate.LocationManager2 = locationsandtax.LocationManager2 ?? locationtaxupdate.LocationManager2;
            await _locationsandtax.UpdateLocationTax(locationtaxupdate);
            return Ok(new { message = "Location Tax updated successfully!" });
        }
    }
}
