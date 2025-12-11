using JetwaysAdmin.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace JetwaysAdmin.WebAPI.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class AdminBookingController : Controller
    {
        private readonly IAdminBookingRepository _bookingRepo;

        public AdminBookingController(IAdminBookingRepository bookingRepo)
        {
            _bookingRepo = bookingRepo;
        }
       
        public IActionResult Index()
        {
            
            return View();
        }

        [HttpGet("Getbookingdata")]
        public async Task<IActionResult> bookingdata()
        {
            var bookings = await _bookingRepo.GetAllAsync();
            return View(bookings);
        }
        public async Task<IActionResult> Details(string bookingId)
        {
            var booking = await _bookingRepo.GetByBookingIdAsync(bookingId);
            if (booking == null)
                return NotFound();

            return View(booking);
        }

    }
}
