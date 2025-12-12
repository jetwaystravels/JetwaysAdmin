using JetwaysAdmin.Entity;
using System;
using System.Collections.Generic;

namespace JetwaysAdmin.UI.ViewModel
{
    public class DashboardbookingViewModel
    {
        // Summary cards (computed on UI side)
        public int TotalBookings { get; set; }
        public decimal TotalRevenue { get; set; }
        public int BookingsLast30Days { get; set; }
        public decimal AverageTicketValue { get; set; }

        // Charts
        public List<ChartPoint> BookingsByMonth { get; set; } = new();
        public List<ChartPoint> TopRoutes { get; set; } = new();

        // Table
        public List<BookingListDto> Bookings { get; set; } = new();

        // Filters
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? CompanyName { get; set; }
        public string? Consultant { get; set; }

        // Pagination
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public bool IsLastPage { get; set; } = true;
    }

    public class ChartPoint
    {
        public string Label { get; set; } = "";
        public decimal Value { get; set; }
    }

    // Must match your API /filtered response
   
}
