using JetwaysAdmin.Entity;
using JetwaysAdmin.UI.ApplicationUrl;
using JetwaysAdmin.UI.Controllers.CustomeFilter;
using JetwaysAdmin.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace JetwaysAdmin.UI.Controllers
{
    public class ManageStaffController : Controller
    {

        //[ServiceFilter(typeof(LogActionFilter))]
        public async Task<IActionResult> ShowManageStaff(int IdLegal, string LegalEntityCode, string LegalEntityName)
        {
            ViewBag.LegalEntityCode = LegalEntityCode;
            ViewBag.LegalEntityName = LegalEntityName;
            ViewBag.Id = IdLegal;
            List<InternalUsers> customeremployee = new List<InternalUsers>();
            BookingConsultantDto bookingConsultant = null;
            // List<BookingConsultantDto> bookingConsultants = new();
            using (HttpClient client = new HttpClient())
            {
                var requestUrl = $"{AppUrlConstant.GetBookingConsultants}?legalEntityCode={Uri.EscapeDataString(LegalEntityCode)}";
                var response1 = await client.GetAsync(requestUrl);
               
                if (response1.IsSuccessStatusCode)
                {
                    var result1 = await response1.Content.ReadAsStringAsync();
                    bookingConsultant = JsonConvert.DeserializeObject<BookingConsultantDto>(result1);
                }

                var response = await client.GetAsync(AppUrlConstant.GetInternalusers);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    customeremployee = JsonConvert.DeserializeObject<List<InternalUsers>>(result);
                }
               
                if (bookingConsultant != null && !string.IsNullOrEmpty(bookingConsultant.BookingConsultantNames))
                {
                    var consultantNames = bookingConsultant.BookingConsultantNames
                        .Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => x.Trim())
                        .ToHashSet(StringComparer.OrdinalIgnoreCase);
                    var bookingConsultantWithIds = customeremployee
                        .Select(emp => new
                        {
                            FullName = $"{emp.FirstName?.Trim()} {emp.LastName?.Trim()}".Trim(),
                            emp.UserID
                        })
                        .Where(x => consultantNames.Contains(x.FullName))
                        .Select(x => $"{x.UserID}|{x.FullName}")
                        .ToList();

                    bookingConsultant.BookingConsultantNames = string.Join(",", bookingConsultantWithIds);
                    customeremployee = customeremployee
                        .Where(emp =>
                        {
                            var fullName = $"{emp.FirstName?.Trim()} {emp.LastName?.Trim()}".Trim();
                            return !consultantNames.Contains(fullName);
                        })
                        .ToList();
                }

                var viewModel = new MenuHeaddata
                {
                    InternalUsers = customeremployee,
                    BookingConsultants = bookingConsultant ?? new BookingConsultantDto()
                };

                return View(viewModel);
            }
        }



        [HttpPost]
        //[ServiceFilter(typeof(LogActionFilter))]
        public async Task<IActionResult> ManageStaff([FromForm] CustomerManageStaff customermanagestaff, int IdLegal, string LegalEntityCode, string LegalEntityName)
        {
            ViewBag.LegalEntityCode = LegalEntityCode;
            ViewBag.LegalEntityName = LegalEntityName;
            ViewBag.Id = IdLegal;
            string employeeData = customermanagestaff.BookingConsultant;
            var employeeID = employeeData
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(emp => emp.Split('-')[0].Trim())
                .ToList();

            customermanagestaff.BookingConsultant = string.Join(",", employeeID);
            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(customermanagestaff);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(AppUrlConstant.ManageStaff, content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["DataUpdate"] = "User assign successfully";
                }
                else
                {
                    TempData["ErrorMessage"] = "User not assign";
                }
                return RedirectToAction("ShowManageStaff", new { IdLegal = IdLegal, LegalEntityCode = LegalEntityCode, LegalEntityName = LegalEntityName });
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveBookingConsultant(int IdLegal, string LegalEntityCode, string LegalEntityName, int EmployeeId)
        {
            ViewBag.LegalEntityCode = LegalEntityCode;
            ViewBag.LegalEntityName = LegalEntityName;
            ViewBag.Id = IdLegal;

            using (HttpClient client = new HttpClient())
            {
                string url = $"{AppUrlConstant.RemoveBookingConsultant}" +
                             $"?legalEntityCode={LegalEntityCode}&employeeId={EmployeeId}";

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, url);
                HttpResponseMessage response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                    return Ok();   
                else
                    return BadRequest();
            }
        }

    }
}
