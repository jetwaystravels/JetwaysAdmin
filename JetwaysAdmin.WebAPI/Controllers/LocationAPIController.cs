using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace JetwaysAdmin.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LocationAPIController : ControllerBase
	{
		private readonly ILocation<AddressCountryState> _locationService;
     
        public LocationAPIController(ILocation<AddressCountryState> locationService)
		{
			_locationService = locationService;
		}

		[HttpGet]
        [Route("countries")]
        public IActionResult GetCountries()
		{
			var data = _locationService.GetAll();
			return Ok(data.Countries); 
		}

		[HttpGet("states/{countryId}")]
		public IActionResult GetStates(int countryId)
		{
			var data = _locationService.GetByCountryId(countryId);
			return Ok(data.States); 
		}

		[HttpGet("cities/{stateId}")]
		public IActionResult GetCities(int stateId)
		{
			var data = _locationService.GetByStateId(stateId);
			return Ok(data.Cities); 
		}
        [HttpGet]
        [Route("states")]
        public IActionResult GetAllStates()
        {
            var data = _locationService.GetAllStates();
            return Ok(data.States);
        }
        [HttpGet]
        [Route("Cities")]
        public IActionResult GetAllCity()
        {
            var data = _locationService.GetAllCity();
            return Ok(data.Cities);
        }
        // GET api/LegalEntity/state/WI00006
        [HttpGet]
        [Route("legalEntitystate")]
        public async Task<ActionResult<AddressCountryState>> GetStatebylegal(string legalEntityCode)
        {
            if (string.IsNullOrWhiteSpace(legalEntityCode))
                return BadRequest("LegalEntityCode is required.");

            var state = await _locationService.GetStatebylegalentityAsync(legalEntityCode); // ✅ await

            if (state is null)
                return NotFound($"No state found for LegalEntityCode {legalEntityCode}");

            return Ok(state); // ✅ serializes AddressCountryState, not Task
        }

    }
}
