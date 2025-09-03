using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Migrations;
using JetwaysAdmin.UI.ApplicationUrl;
using JetwaysAdmin.UI.Controllers.CustomeFilter;
using JetwaysAdmin.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;
using System.Xml.Linq;

namespace JetwaysAdmin.UI.Controllers
{
    public class OrganizationStructureController : Controller
    {
        public async Task<IActionResult> ShowOrganization(int IdLegal, string legalEntityName, string legalEntityCode)
        {
            List<LegalEntity> rootEntity = new List<LegalEntity>();
            List<CustomersEmployee> employees = new List<CustomersEmployee>();

            using (HttpClient client = new HttpClient())
            {
                string apiLegalEntityUrl = $"{AppUrlConstant.GetLegalEntity}";
                HttpResponseMessage response = await client.GetAsync(apiLegalEntityUrl);

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        var responseObject = JsonConvert.DeserializeObject<LegalEntityResponse>(result);
                        rootEntity = responseObject?.Data;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error during deserialization: " + ex.Message);
                    }
                    var parent = rootEntity.FirstOrDefault(e => e.LegalEntityCode == legalEntityCode);
                    if (parent == null)
                        return View(new List<LegalEntity>());
                    var children = rootEntity
                        .Where(e => e.ParentLegalEntityCode == legalEntityCode)
                        .ToList();
                    var combined = new List<LegalEntity> { parent };
                    combined.AddRange(children);
                    // 2) Fetch employees by legalEntityCode via your API
                    try
                    {
                        // If you have a constant, prefer that:
                         string apiEmployeeUrl = $"{AppUrlConstant.GetCustomerEmployee}?LegalEntityCode={Uri.EscapeDataString(legalEntityCode)}";
                         var empResponse = await client.GetAsync(apiEmployeeUrl);
                        if (empResponse.IsSuccessStatusCode)
                        {
                            var empJson = await empResponse.Content.ReadAsStringAsync();

                            // If your API returns a plain array:  [ {...}, {...} ]
                            employees = JsonConvert.DeserializeObject<List<CustomersEmployee>>(empJson) ?? new List<CustomersEmployee>();

                            // If your API wraps data like { "data": [...] }, use this instead:
                            // var empObj = JsonConvert.DeserializeObject<CustomersEmployeeResponse>(empJson);
                            // employees = empObj?.Data ?? new List<CustomersEmployee>();
                        }
                        else
                        {
                            Console.WriteLine($"Employee API failed: {(int)empResponse.StatusCode} {empResponse.ReasonPhrase}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error calling Employee API: " + ex.Message);
                    }

                    // 3) Sort and count (optional)
                    employees = employees
                        .Where(x => x.AppStatus == 0)                 // keep your filter if needed
                        .OrderBy(x => x.UserID)                       // same as your EF query
                        .ToList();

                    ViewBag.EmployeeCount = employees.Count;
                    ViewBag.Employees = employees;                    // if you want to render the list
                    ViewBag.LegalEntityCode = legalEntityCode;
                    ViewBag.LegalEntityName = legalEntityName;
                    ViewBag.Id = IdLegal;
                    return View(combined);
                }
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ShowOffice(int IdLegal, string LegalEntityCode, string LegalEntityName)
        {
            ViewBag.LegalEntityCode = LegalEntityCode;
            ViewBag.LegalEntityName = LegalEntityName;
            ViewBag.Id = IdLegal;
            var iataGroups = new List<IATAGroupView>();
            var countryList = new List<Country>();
            List<LocationsandTax> locationsandtax = new List<LocationsandTax>();
            List<AddSupplier> supplier = new List<AddSupplier>();
            List<InternalUsers> customeremployee = new List<InternalUsers>();
            BookingConsultantDto bookingConsultant = null;
            using (HttpClient client = new HttpClient())
            {
                var iataResponse = await client.GetAsync(AppUrlConstant.GetIATAGroup);
                if (iataResponse.IsSuccessStatusCode)
                {
                    var result = await iataResponse.Content.ReadAsStringAsync();
                    iataGroups = JsonConvert.DeserializeObject<List<IATAGroupView>>(result);
                }

                var countryResponse = await client.GetAsync(AppUrlConstant.GetCountry);
                if (countryResponse.IsSuccessStatusCode)
                {
                    var result = await countryResponse.Content.ReadAsStringAsync();
                    countryList = JsonConvert.DeserializeObject<List<Country>>(result);
                }
                //string requestUrl = $"{AppUrlConstant.GetLoactionTaxAll}";
                string requestUrl = $"{AppUrlConstant.GetLoactionTax}?LegalEntityCode={LegalEntityCode}";
                var userresponse = await client.GetAsync(requestUrl);
                if (userresponse.IsSuccessStatusCode)
                  {
                  var result = await userresponse.Content.ReadAsStringAsync();
                  locationsandtax = JsonConvert.DeserializeObject<List<LocationsandTax>>(result);
                 }
                var url = $"{AppUrlConstant.GetSuppliersLegalEntity}?legalEntityCode={LegalEntityCode}";
                var suppresponse = await client.GetAsync(url);
                if (suppresponse.IsSuccessStatusCode)
                {
                    var result = await suppresponse.Content.ReadAsStringAsync();
                    supplier = JsonConvert.DeserializeObject<List<AddSupplier>>(result);
                }
                //Manage Staff
                var RequestUrl = $"{AppUrlConstant.GetBookingConsultants}?legalEntityCode={Uri.EscapeDataString(LegalEntityCode)}";
                var response1 = await client.GetAsync(RequestUrl);

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
                        .Select(name => name.Trim())
                        .ToHashSet(StringComparer.OrdinalIgnoreCase); // Case-insensitive

                    customeremployee = customeremployee
                        .Where(emp =>
                        {
                            var fullName = $"{emp.FirstName?.Trim()} {emp.LastName?.Trim()}".Trim();
                            return !consultantNames.Contains(fullName);
                        })
                        .ToList();
                }

            }

            var viewModel = new MenuHeaddata
            {
                IATAGruopName = iataGroups,
                LocationandTax = locationsandtax,
                getsupplier = supplier,
                InternalUsers = customeremployee,
                BookingConsultants = bookingConsultant ?? new BookingConsultantDto()
            };
            ViewBag.CountryList = countryList;
            return PartialView("_OfficeModel", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> LoadStates(int CountryId)
        {
            List<State> states = new();

            using (HttpClient client = new HttpClient())
            {
                string url = $"{AppUrlConstant.GetSate}/{CountryId}";
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    states = JsonConvert.DeserializeObject<List<State>>(json);
                }
            }

            return Json(states);
        }
        [HttpGet]
        public async Task<IActionResult> LoadCities(int stateId)
        {
            List<City> cities = new();

            using (HttpClient client = new HttpClient())
            {
                string url = $"{AppUrlConstant.GetCity}/{stateId}";
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    cities = JsonConvert.DeserializeObject<List<City>>(json);
                }
            }
            return Json(cities);
        }

        [HttpPost]
        public async Task<IActionResult> AddOffice([FromForm] LegalEntity legalEntity,int IdLegal, string ParentLegalEntityCode, string ParentLegalEntityName)
        {
            ViewBag.LegalEntityCode = ParentLegalEntityCode;
            ViewBag.LegalEntityName = ParentLegalEntityName;
            ViewBag.Id = IdLegal;
            using (HttpClient client = new HttpClient())
            {
                var legalentityalldata = await client.GetAsync(AppUrlConstant.GetLegalEntity);
                if (legalentityalldata.IsSuccessStatusCode)
                {
                    var existingJson = await legalentityalldata.Content.ReadAsStringAsync();
                    var parsedResult = JsonConvert.DeserializeObject<LegalEntityResponse>(existingJson);
                    var existingData = parsedResult?.Data ?? new List<LegalEntity>();
                    bool nameDup = existingData.Any(e =>
                    string.Equals(e.LegalEntityName?.Trim(), legalEntity.LegalEntityName?.Trim(), StringComparison.OrdinalIgnoreCase));
                    bool codeDup = existingData.Any(e =>
                    string.Equals(e.LegalEntityCode?.Trim(), legalEntity.LegalEntityCode?.Trim(), StringComparison.OrdinalIgnoreCase));
                    bool corpDup = !string.IsNullOrWhiteSpace(legalEntity.CorporateAccountsCode) &&
                         existingData.Any(e =>
                             string.Equals(e.CorporateAccountsCode?.Trim(),
                                           legalEntity.CorporateAccountsCode?.Trim(),
                                           StringComparison.OrdinalIgnoreCase));

                    bool isDuplicate = nameDup || codeDup || corpDup;
                    if (isDuplicate)
                    {
                        TempData["DuplicateError"] = "Duplicate entry found! Please enter unique Legal Entity Name, Code, or Corporate Account Code.";
                        return RedirectToAction("ShowOrganization", new { LegalEntityCode = ParentLegalEntityCode, LegalEntityName = ParentLegalEntityName });
                    }
                }
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(legalEntity);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsJsonAsync(AppUrlConstant.AddLegalEntity, legalEntity);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    TempData["LegalAdd"] = "Office Add Successfully";
                    TempData["OpenOfficeModal"] = true;
                    return Json(new
                    {
                        success = true,
                        url = Url.Action(nameof(ShowOffice), new
                        {
                            IdLegal,
                            LegalEntityCode = ParentLegalEntityCode,
                            LegalEntityName = ParentLegalEntityName
                        })
                    });
                }
                ViewBag.ErrorMessage = "Data not  insert";
              
                return RedirectToAction("ShowOrganization", new { LegalEntityCode = ParentLegalEntityCode, LegalEntityName = ParentLegalEntityName });
            }
        }

        public async Task<IActionResult> UpdateOffice(int Id, int IdLegal, string LegalEntityCode,string LegalEntityCodeParent, string LegalEntityName)
        {
            List<IATAGroupView> iataGroups = new();
            List<Country> countryList = new();
            LegalEntity entity = null;
            List<AddSupplier> supplier = new List<AddSupplier>();
            List<InternalUsers> customeremployee = new List<InternalUsers>();
            List<LocationsandTax> locationsandtax = new List<LocationsandTax>();
            BookingConsultantDto bookingConsultant = null;
            using (HttpClient client = new HttpClient())
            {
                string url = $"{AppUrlConstant.GetLegalEntityID}/{Id}";
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    entity = JsonConvert.DeserializeObject<LegalEntity>(result);
                }
           
                var iataResponse = await client.GetAsync(AppUrlConstant.GetIATAGroup);
                if (iataResponse.IsSuccessStatusCode)
                {
                    var result = await iataResponse.Content.ReadAsStringAsync();
                    iataGroups = JsonConvert.DeserializeObject<List<IATAGroupView>>(result);
                }

                var countryResponse = await client.GetAsync(AppUrlConstant.GetCountry);
                if (countryResponse.IsSuccessStatusCode)
                {
                    var result = await countryResponse.Content.ReadAsStringAsync();
                    countryList = JsonConvert.DeserializeObject<List<Country>>(result);
                }
                var Url = $"{AppUrlConstant.GetSuppliersLegalEntity}?legalEntityCode={LegalEntityCode}";
                var suppresponse = await client.GetAsync(Url);
                if (suppresponse.IsSuccessStatusCode)
                {
                    var result = await suppresponse.Content.ReadAsStringAsync();
                    supplier = JsonConvert.DeserializeObject<List<AddSupplier>>(result);
                } 
            
                //Manage Staff
                var RequestUrl = $"{AppUrlConstant.GetBookingConsultants}?legalEntityCode={Uri.EscapeDataString(LegalEntityCode)}";
                var response1 = await client.GetAsync(RequestUrl);
                if (response1.IsSuccessStatusCode)
                {
                    var result1 = await response1.Content.ReadAsStringAsync();
                    bookingConsultant = JsonConvert.DeserializeObject<BookingConsultantDto>(result1);
                }
                string requestUrl = $"{AppUrlConstant.GetLoactionTax}?LegalEntityCode={LegalEntityCode}";
                var userresponse = await client.GetAsync(requestUrl);
                if (userresponse.IsSuccessStatusCode)
                {
                    var result = await userresponse.Content.ReadAsStringAsync();
                    locationsandtax = JsonConvert.DeserializeObject<List<LocationsandTax>>(result);
                }
                var ResponseI = await client.GetAsync(AppUrlConstant.GetInternalusers);

                if (ResponseI.IsSuccessStatusCode)
                {
                    var result = await ResponseI.Content.ReadAsStringAsync();
                    customeremployee = JsonConvert.DeserializeObject<List<InternalUsers>>(result);
                }
                if (bookingConsultant != null && !string.IsNullOrEmpty(bookingConsultant.BookingConsultantNames))
                {
                    var consultantNames = bookingConsultant.BookingConsultantNames
                        .Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(name => name.Trim())
                        .ToHashSet(StringComparer.OrdinalIgnoreCase); // Case-insensitive

                    customeremployee = customeremployee
                        .Where(emp =>
                        {
                            var fullName = $"{emp.FirstName?.Trim()} {emp.LastName?.Trim()}".Trim();
                            return !consultantNames.Contains(fullName);
                        })
                        .ToList();
                }
              
            }
            ViewBag.LegalEntityCode = LegalEntityCodeParent;
            ViewBag.LegalEntityName = LegalEntityName;
            ViewBag.Id = IdLegal;
            ViewBag.CountryList = countryList;
            var viewModel = new MenuHeaddata
            {
                LegalEntitydata = entity != null ? new List<LegalEntity> { entity } : new List<LegalEntity>(),
                IATAGruopName = iataGroups,
                getsupplier = supplier,
                InternalUsers = customeremployee,
                LocationandTax = locationsandtax,
                BookingConsultants = bookingConsultant ?? new BookingConsultantDto()
            };
            return PartialView("_OfficeUpdate", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditOffice(LegalEntity legalEntity, int  IdLegal, string ParentLegalEntityCode, string ParentLegalEntityName)
        {
            int Id = legalEntity.Id;
            using (HttpClient client = new HttpClient())
            {
                ViewBag.LegalEntityCode = ParentLegalEntityCode;
                ViewBag.LegalEntityName = ParentLegalEntityName;
                ViewBag.Id = IdLegal;
                string Data = JsonConvert.SerializeObject(legalEntity);
                StringContent content = new StringContent(Data, Encoding.UTF8, "application/json");
                HttpResponseMessage response =client.PutAsync(AppUrlConstant.EditLegalEntityID + "/" + legalEntity.Id, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["officeupdate_message"] = "Office Update";
                    return RedirectToAction("ShowOrganization", new
                    {
                        IdLegal = IdLegal,
                        legalEntityCode = ParentLegalEntityCode,  
                        legalEntityName = ParentLegalEntityName
                    });
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetSupplierCredential(int SupplierId, int IdLegal, string LegalEntityCode, string LegalEntityName)
        {
            List<SuppliersCredential> allCredentials = new List<SuppliersCredential>();
            List<SuppliersCredential> filteredCredentials = new List<SuppliersCredential>();
            List<DealCode> dealcode = new List<DealCode>();
            List<DealCode> filtereddealcode = new List<DealCode>();
            
            string flightclass = "Corporate"; // Set the flight class as needed
            using (HttpClient client = new HttpClient())
            {
                var url = $"{AppUrlConstant.GetSupplierCredential}?flightclass={Uri.EscapeDataString(flightclass)}";
                HttpResponseMessage response = await client.GetAsync(url);
               
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    allCredentials = JsonConvert.DeserializeObject<List<SuppliersCredential>>(result);
                    filteredCredentials = allCredentials
                   .Where(c => c.SupplierId.HasValue && c.SupplierId.Value == SupplierId)
        .ToList();
                }

                //url = $"{AppUrlConstant.GetDealCodeSupplierId}/?SupplierId={SupplierId}";
                url = $"{AppUrlConstant.GetcustomerDealCode}/?SupplierId={SupplierId}&LegalEntityCode={LegalEntityCode}";
               var dealresponse = await client.GetAsync(url);
                if (dealresponse.IsSuccessStatusCode)
                {
                    var result = await dealresponse.Content.ReadAsStringAsync();

                    // Fix for CS0029 and CS8600  
                    dealcode = JsonConvert.DeserializeObject<List<CustomerDealCode>>(result)
                       ?.Select(c => new DealCode
                       {
                           DealCodeId = c.DealCodeID,
                           PCC = null, // Map appropriately if needed  
                           DealCodeName = c.DealCodeName,
                           TravelMode = null, // Map appropriately if needed  
                           DealPricingCode = c.DealPricingCode,
                           TourCode = c.TourCode,
                           AssociatedFareTypes = c.AssociatedFareTypes,
                           CabinClass = c.ClassOfSeats,
                           DefaultValue = null, //Map appropriately if needed  
                           ClassOfSeats = c.ClassOfSeats,
                           SupplierId = c.SupplierId,
                           AutoEnableDealCode = null, //Map appropriately if needed  
                           GSTMandatory = c.GstMandatory,
                           OverrideCustomerGST = null, //Map appropriately if needed  
                           BookingType = c.BookingType,
                           StartDate = c.StartDate ?? DateTime.MinValue, //Handle nullability  
                           ExpiryDate = c.EndDate
                       }).ToList() ?? new List<DealCode>();

                    // Fix for CS0029 and CS8600  

                    filtereddealcode = dealcode
                    .Where(c => c.SupplierId.HasValue && c.SupplierId.Value == SupplierId).ToList();
                }
            }
            var dealCode = new MenuHeaddata
            {
                supplierscredential = filteredCredentials,
                DealCodeView = filtereddealcode

            };
            ViewBag.SupplierId = SupplierId;
            ViewBag.LegalEntityCode = LegalEntityCode;
            ViewBag.LegalEntityName = LegalEntityName;
            ViewBag.Id = IdLegal;
            return PartialView("_CredentialPartial", dealCode);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateSupplierAppStatus(bool appStatus, int supplierId, string legalEntityCode, string legalEntityCodeparent,  string legalEntityName)
        {
            using (HttpClient client = new HttpClient())
            {
                //Prepare payload
                var payload = new
                {
                    LegalEntityCode = legalEntityCode,
                    SupplierID = supplierId,
                    IsActive = appStatus
                };
                string json = JsonConvert.SerializeObject(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                //Correct POST URL (no query params)
                string url = $"{AppUrlConstant.Updatelegalentitysupplierstatus}";
                HttpResponseMessage response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["supplier_message"] = "Status Updated";
                    return RedirectToAction("ShowOrganization", new { LegalEntityCode = legalEntityCodeparent, LegalEntityName = legalEntityName });
                }
            }

            TempData["supplier_message"] = "Failed to update status";
            return RedirectToAction("ShowOrganization", new { LegalEntityCode = legalEntityCodeparent, LegalEntityName = legalEntityName });
        }



        [HttpPost]
        public async Task<IActionResult> AddcustomerDealCodes(CustomerDealCode dealcode, int supplierId, int IdLegal, string LegalEntityCode, string LegalEntityName)
        {
            using (HttpClient client = new HttpClient())
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(dealcode);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsJsonAsync(AppUrlConstant.AddcustomerDealCode, dealcode);
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessagedealcode"] = "Deal code saved successfully!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to save deal code. Please try again.";
                }
            }
            ViewBag.SupplierId = supplierId;
            ViewBag.LegalEntityCode = LegalEntityCode;
            ViewBag.LegalEntityName = LegalEntityName;
            ViewBag.Id = IdLegal;
            return RedirectToAction("ShowOrganization", new
            {
                Id = IdLegal,
                LegalEntityCode = LegalEntityCode,
                LegalEntityName = LegalEntityName
            });
        }
        
        [HttpPost]
        [ServiceFilter(typeof(LogActionFilter))]
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
                    TempData["ManageDataUpdate"] = "User assign successfully";
                }
                else
                {
                    TempData["ErrorMessage"] = "User not assign";
                }
                return Json(new
                {
                    success = true,
                    url = Url.Action(nameof(ShowOffice), new
                    {
                        IdLegal,
                        LegalEntityCode,
                        LegalEntityName
                    })
                });
                
            }
        }

    }
}