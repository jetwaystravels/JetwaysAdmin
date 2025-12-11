using JetwaysAdmin.Repositories.Interface;
using JetwaysAdmin.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace JetwaysAdmin.UI.Controllers.Booking
{
    public class DashboardBookingController : Controller
    {
        private readonly IAdminBookingRepository _bookingRepo;
        public DashboardBookingController(IAdminBookingRepository bookingRepo)
        {
            _bookingRepo = bookingRepo;
        }

        public async Task<IActionResult> Index()
        {
            var bookings = (await _bookingRepo.GetAllAsync()).ToList();

            var vm = new DashboardbookingViewModel();

            // 1) Summary Cards
            vm.TotalBookings = bookings.Count;
            vm.TotalRevenue = bookings.Sum(b => b.TotalAmount ?? 0);

            var since30 = DateTime.UtcNow.AddDays(-30);
            vm.BookingsLast30Days = bookings.Count(b => b.BookedDate.HasValue && b.BookedDate.Value >= since30);

            vm.AverageTicketValue = vm.TotalBookings > 0
                ? vm.TotalRevenue / vm.TotalBookings
                : 0;

            // 2) Bookings by Month chart
            vm.BookingsByMonth = bookings
                .Where(b => b.BookedDate.HasValue)
                .GroupBy(b => new { b.BookedDate!.Value.Year, b.BookedDate!.Value.Month })
                .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month)
                .Select(g => new ChartPoint
                {
                    Label = $"{g.Key.Month:D2}/{g.Key.Year}",
                    Value = g.Count()
                })
                .ToList();

            // 3) Top 5 routes (Origin-Destination)
            vm.TopRoutes = bookings
                .GroupBy(b => $"{b.Origin}-{b.Destination}")
                .OrderByDescending(g => g.Count())
                .Take(5)
                .Select(g => new ChartPoint
                {
                    Label = g.Key,
                    Value = g.Count()
                })
                .ToList();

            // 4) Recent bookings table
            vm.RecentBookings = bookings
                .OrderByDescending(b => b.BookedDate)
                .Take(20)
                .ToList();

            return View(vm);
        }
    }
}
