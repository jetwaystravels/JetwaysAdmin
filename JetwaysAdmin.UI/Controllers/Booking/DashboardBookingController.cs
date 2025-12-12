using JetwaysAdmin.Entity;
using JetwaysAdmin.UI.ApplicationUrl;
using JetwaysAdmin.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using ClosedXML.Excel;

namespace JetwaysAdmin.UI.Controllers.Booking
{
    public class DashboardBookingController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index(
            DateTime? fromDate,
            DateTime? toDate,
            string? companyName,
            string? consultant,
            int pageNumber = 1,
            int pageSize = 20)
        {
            var vm = new DashboardbookingViewModel
            {
                FromDate = fromDate,
                ToDate = toDate,
                CompanyName = companyName,
                Consultant = consultant,
                PageNumber = pageNumber < 1 ? 1 : pageNumber,
                PageSize = pageSize < 1 ? 20 : pageSize
            };

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Build API URL with query params
                    var url =
                        $"{AppUrlConstant.GetBookingFiltered}" +
                        $"?fromDate={(fromDate.HasValue ? fromDate.Value.ToString("yyyy-MM-dd") : "")}" +
                        $"&toDate={(toDate.HasValue ? toDate.Value.ToString("yyyy-MM-dd") : "")}" +
                        $"&companyName={Uri.EscapeDataString(companyName ?? "")}" +
                        $"&consultant={Uri.EscapeDataString(consultant ?? "")}" +
                        $"&pageNumber={vm.PageNumber}" +
                        $"&pageSize={vm.PageSize}";

                    var response = await client.GetAsync(url);

                    if (!response.IsSuccessStatusCode)
                    {
                        TempData["ErrorMessage"] = "Failed to load booking data from API.";
                        return View(vm);
                    }

                    var json = await response.Content.ReadAsStringAsync();
                    var paged = JsonConvert.DeserializeObject<ViewModel.PagedResult<BookingListDto>>(json);
                    var bookings = paged?.Items ?? new List<BookingListDto>();

                    vm.Bookings = bookings;
                    vm.IsLastPage = paged?.IsLastPage ?? true;

                    // Dashboard cards (based on current filtered page)
                    vm.TotalBookings = bookings.Count;
                    vm.TotalRevenue = bookings.Sum(b => b.TotalAmount ?? 0);

                    var since30 = DateTime.UtcNow.AddDays(-30);
                    vm.BookingsLast30Days = bookings.Count(b =>
                        b.BookedDate.HasValue && b.BookedDate.Value >= since30);

                    vm.AverageTicketValue = vm.TotalBookings > 0
                        ? vm.TotalRevenue / vm.TotalBookings
                        : 0;

                    // Bookings by month chart
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

                    // Top routes chart
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

                    return View(vm);
                }
            }
            catch
            {
                TempData["ErrorMessage"] = "API not reachable / SSL issue / JSON error.";
                return View(vm);
            }
        }


        public async Task<IActionResult> ExportExcel( DateTime? fromDate, DateTime? toDate, string? companyName,string? consultant)
        {
            using (HttpClient client = new HttpClient())
            {
                // Export should fetch a larger set. You can increase pageSize.
                // If you want FULL export across all pages, tell me — I’ll give a loop method.
                int pageNumber = 1;
                int pageSize = 5000;

                var url =
                    $"{AppUrlConstant.GetBookingFiltered}" +
                    $"?fromDate={(fromDate.HasValue ? fromDate.Value.ToString("yyyy-MM-dd") : "")}" +
                    $"&toDate={(toDate.HasValue ? toDate.Value.ToString("yyyy-MM-dd") : "")}" +
                    $"&companyName={Uri.EscapeDataString(companyName ?? "")}" +
                    $"&consultant={Uri.EscapeDataString(consultant ?? "")}" +
                    $"&pageNumber={pageNumber}" +
                    $"&pageSize={pageSize}";

                var response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    TempData["ErrorMessage"] = "Excel export failed: API not responding.";
                    return RedirectToAction("Index", new { fromDate, toDate, companyName, consultant });
                }

                var json = await response.Content.ReadAsStringAsync();
                var paged = JsonConvert.DeserializeObject<ViewModel.PagedResult<BookingListDto>>(json);
                var bookings = paged?.Items ?? new List<BookingListDto>();

                using var wb = new XLWorkbook();
                var ws = wb.Worksheets.Add("Bookings");

                // Header
                ws.Cell(1, 1).Value = "BookingID";
                ws.Cell(1, 2).Value = "RecordLocator";
                ws.Cell(1, 3).Value = "BookedDate";
                ws.Cell(1, 4).Value = "Origin";
                ws.Cell(1, 5).Value = "Destination";
                ws.Cell(1, 6).Value = "TotalAmount";
                //ws.Cell(1, 7).Value = "CompanyName";
                //ws.Cell(1, 8).Value = "Consultant";

                // Rows
                int row = 2;
                foreach (var b in bookings)
                {
                    ws.Cell(row, 1).Value = b.BookingID;
                    ws.Cell(row, 2).Value = b.RecordLocator;
                    ws.Cell(row, 3).Value = b.BookedDate?.ToString("yyyy-MM-dd HH:mm");
                    ws.Cell(row, 4).Value = b.Origin;
                    ws.Cell(row, 5).Value = b.Destination;
                    ws.Cell(row, 6).Value = b.TotalAmount ?? 0;
                    //ws.Cell(row, 7).Value = b.CompanyName;
                    //ws.Cell(row, 8).Value = b.Consultant;
                    row++;
                }

                ws.Columns().AdjustToContents();

                using var stream = new System.IO.MemoryStream();
                wb.SaveAs(stream);
                stream.Position = 0;

                var fileName = $"Bookings_{DateTime.Now:yyyyMMdd_HHmm}.xlsx";
                return File(stream.ToArray(),
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    fileName);
            }
        }
        }
}
