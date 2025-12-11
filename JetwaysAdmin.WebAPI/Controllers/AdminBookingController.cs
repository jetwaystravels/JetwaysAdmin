using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JetwaysAdmin.WebAPI.Controllers
{
    // This makes it an API controller and shows it in Swagger
    [ApiController]
    [Route("api/[controller]")]
    public class AdminBookingController : ControllerBase
    {
        private readonly IAdminBookingRepository _bookingRepo;

        public AdminBookingController(IAdminBookingRepository bookingRepo)
        {
            _bookingRepo = bookingRepo;
        }

        /// <summary>
        /// Get all bookings (selected columns only).
        /// </summary>
        /// <returns>List of BookingListDto</returns>
        [HttpGet("bookingdata")]
        public async Task<ActionResult<IEnumerable<BookingListDto>>> GetBookingData()
        {
            var bookings = await _bookingRepo.GetAllAsync();
            return Ok(bookings); // JSON response for Swagger / clients
        }

        /// <summary>
        /// Get a single booking by BookingId (selected columns).
        /// </summary>
        [HttpGet("{bookingId}")]
        public async Task<ActionResult<BookingListDto>> GetBookingById(string bookingId)
        {
            var booking = await _bookingRepo.GetByBookingIdAsync(bookingId);
            if (booking == null)
                return NotFound();

            return Ok(booking);
        }

        // OPTIONAL: if you implemented filtering + pagination via BookingFilter / PagedResult:

        /// <summary>
        /// Get bookings with optional filters and pagination.
        /// </summary>
        [HttpGet("filtered")]
        public async Task<ActionResult<PagedResult<BookingListDto>>> GetFiltered(
            [FromQuery] DateTime? fromDate,
            [FromQuery] DateTime? toDate,
            [FromQuery] string? recordLocator,
            [FromQuery] string? companyName,
            [FromQuery] string? consultant,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20)
        {
            var filter = new BookingFilter
            {
                FromDate = fromDate,
                ToDate = toDate,
                RecordLocator = recordLocator,
                CompanyName = companyName,
                Consultant = consultant,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await _bookingRepo.GetFilteredAsync(filter);
            return Ok(result);
        }
    }
}
