using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Implementations;
using JetwaysAdmin.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace JetwaysAdmin.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DealCodeAPIController : ControllerBase
    {
        private readonly IDealCode<DealCode> _dealCodeService;
        public DealCodeAPIController(IDealCode<DealCode> dealCodeService)
        {
            _dealCodeService = dealCodeService;
        }
        [HttpPost]
        [Route("AddDealCode")]
        public async Task<IActionResult> AddDealCode([FromBody] DealCode dealCode)
        {
            if (dealCode == null)
            {
                return BadRequest("Deal code cannot be null");
            }
            await _dealCodeService.AddDealCode(dealCode);
            return Ok("Deal code added successfully");
        }
        [HttpGet]
        [Route("GetDealCode")]
        public async Task<ActionResult<IEnumerable<DealCode>>> GetDealCode()
        {

           var getdealCode = await _dealCodeService.GetDealCode();
            return Ok(getdealCode);
        }

        [HttpGet]
        [Route("GetDealCodeSupplierId")]
        public async Task<ActionResult<IEnumerable<DealCode>>> GetDealCodeSupplierId(int SupplierId)
        {

            var getdealCode = await _dealCodeService.GetDealCodeSupplierId(SupplierId);
            return Ok(getdealCode);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DealCode>> GetDealCodeById(int id)
        {
            var dealcode = await _dealCodeService.GetDealCodeById(id);
            if (dealcode == null)
            {
                return NotFound();
            }
            return Ok(dealcode);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDealCode(int DealCodeId, DealCode dealcode)
        {
            DealCodeId = dealcode.DealCodeId;
            if (DealCodeId != dealcode.DealCodeId)
            {
                return BadRequest("DealCode ID mismatch.");
            }
            var dealcodeupdate = await _dealCodeService.GetDealCodeById(DealCodeId);
            if (dealcodeupdate == null)
            {
                return NotFound();
            }
            dealcodeupdate.CabinClass = dealcode.CabinClass ?? dealcodeupdate.CabinClass;
            dealcodeupdate.PCC = dealcode.PCC ?? dealcodeupdate.PCC;
            dealcodeupdate.DealCodeName = dealcode.DealCodeName ?? dealcodeupdate.DealCodeName;
           // dealcodeupdate.SelectedCredentialId = dealcode.SelectedCredentialId ?? dealcodeupdate.SelectedCredentialId;
            dealcodeupdate.TravelMode = dealcode.TravelMode ?? dealcodeupdate.TravelMode;
            dealcodeupdate.CabinClass = dealcode.CabinClass ?? dealcodeupdate.CabinClass;
            dealcodeupdate.DealPricingCode = dealcode.DealPricingCode ?? dealcodeupdate.DealPricingCode;
            dealcodeupdate.TourCode = dealcode.TourCode ?? dealcodeupdate.TourCode;
            dealcodeupdate.AssociatedFareTypes = dealcode.AssociatedFareTypes ?? dealcodeupdate.AssociatedFareTypes;
            dealcodeupdate.DefaultValue = dealcode.DefaultValue ?? dealcodeupdate.DefaultValue;
            dealcodeupdate.ExpiryDate = dealcode.ExpiryDate ?? dealcodeupdate.ExpiryDate;
            dealcodeupdate.ClassOfSeats = dealcode.ClassOfSeats ?? dealcodeupdate.ClassOfSeats;
            dealcodeupdate.AutoEnableDealCode = dealcode.AutoEnableDealCode ?? dealcodeupdate.AutoEnableDealCode;
            dealcodeupdate.GSTMandatory = dealcode.GSTMandatory ?? dealcodeupdate.GSTMandatory;
            dealcodeupdate.OverrideCustomerGST = dealcode.OverrideCustomerGST ?? dealcodeupdate.OverrideCustomerGST;
            dealcodeupdate.BookingType = dealcode.BookingType ?? dealcodeupdate.BookingType;
            await _dealCodeService.UpdateDealCodeById(dealcodeupdate);
            return Ok(new { message = "DealCode updated successfully!" });
        }

        [HttpGet("GetcustomerDealCode")]
        public async Task<IActionResult> GetcustomerDealCode([FromQuery] int supplierId, [FromQuery] string legalEntityCode)
        {
            if (supplierId <= 0 || string.IsNullOrEmpty(legalEntityCode))
            {
                return BadRequest("SupplierId and LegalEntityCode are required.");
            }

            var dealCodes = await _dealCodeService.GetcustomerDealCode(supplierId, legalEntityCode);

            if (dealCodes == null || !dealCodes.Any())
            {
                return NotFound("No deal codes found for the given parameters.");
            }

            return Ok(dealCodes);
        }


        [HttpPost("AddcustomerDealCode")]
        public async Task<IActionResult> AddCustomerDealCode([FromBody] CustomerDealCode dealCode)
        {
            if (dealCode == null)
                return BadRequest("Invalid data");

            await _dealCodeService.AddCustomerDealCode(dealCode);

            return Ok(new { message = "Customer Deal Code added successfully", id = dealCode.DealCodeID });
        }
    }
}
