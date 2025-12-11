using DocumentFormat.OpenXml.Drawing.Wordprocessing;

namespace JetwaysAdmin.UI.ViewModel
{
    public class DashboardbookingViewModel
    {
        // Summary cards
        public int TotalBookings { get; set; }
        public decimal TotalRevenue { get; set; }
        public int BookingsLast30Days { get; set; }
        public decimal AverageTicketValue { get; set; }

        // Charts
        public List<ChartPoint> BookingsByMonth { get; set; } = new();
        public List<ChartPoint> TopRoutes { get; set; } = new();

        // Optional: latest bookings table
        public List<JetwaysAdmin.Entity.BookingListDto> RecentBookings { get; set; } = new();
    }
    public class ChartPoint
    {
        public string Label { get; set; } = string.Empty;
        public decimal Value { get; set; }
    }
}
