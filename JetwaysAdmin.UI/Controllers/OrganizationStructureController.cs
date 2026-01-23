using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Migrations;
using JetwaysAdmin.UI.ApplicationUrl;
using JetwaysAdmin.UI.Controllers.CustomeFilter;
using JetwaysAdmin.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Mono.TextTemplating;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;
using System.Xml.Linq;
using System.IO;
using OfficeOpenXml;
using ClosedXML.Excel;
using JetwaysAdmin.Repositories.Interface;

namespace JetwaysAdmin.UI.Controllers
{
    public class OrganizationStructureController : Controller
    {
        //private dynamic groupedEmployees;
        public async Task<IActionResult> ShowOrganization(int IdLegal, string legalEntityName, string legalEntityCode)
        {
            // Helpers
            string Norm(string s) => (s ?? string.Empty).Trim().ToUpperInvariant();
            int ParseToInt(string? v) => int.TryParse((v ?? "").Trim(), out var n) ? n : 0;

            var rootEntity = new List<LegalEntity>();
            var states = new List<JetwaysAdmin.Entity.State>();
            var cities = new List<City>();

            try
            {
                using (var client = new HttpClient())
                {
                    // ---- States ----
                    var stateResp = await client.GetAsync($"{AppUrlConstant.GetSate}");
                    if (stateResp.IsSuccessStatusCode)
                    {
                        var stateresult = await stateResp.Content.ReadAsStringAsync();
                        states = JsonConvert.DeserializeObject<List<JetwaysAdmin.Entity.State>>(stateresult) ?? new List<JetwaysAdmin.Entity.State>();
                    }

                    // ---- Cities ----
                    var cityResp = await client.GetAsync($"{AppUrlConstant.GetCities}");
                    if (cityResp.IsSuccessStatusCode)
                    {
                        var cityJson = await cityResp.Content.ReadAsStringAsync();
                        cities = JsonConvert.DeserializeObject<List<City>>(cityJson) ?? new List<City>();
                    }

                    // ---- Legal Entities (parent + children) ----
                    var leResp = await client.GetAsync($"{AppUrlConstant.GetLegalEntity}");
                    if (!leResp.IsSuccessStatusCode)
                        return View(new List<LegalEntity>());

                    var leJson = await leResp.Content.ReadAsStringAsync();
                    var leObj = JsonConvert.DeserializeObject<LegalEntityResponse>(leJson);
                    rootEntity = leObj?.Data ?? new List<LegalEntity>();

                    var parent = rootEntity.FirstOrDefault(e => e.LegalEntityCode == legalEntityCode);
                    if (parent == null) return View(new List<LegalEntity>());

                    var children = rootEntity.Where(e => e.ParentLegalEntityCode == legalEntityCode).ToList();
                    var combined = new List<LegalEntity> { parent };
                    combined.AddRange(children);

                    // ---- Enrich city/state names ----
                    combined = combined.Select(le =>
                    {
                        var stateId = ParseToInt(le.State);
                        var cityId = ParseToInt(le.City);

                        var cityRow = cities.FirstOrDefault(c => c.StateID == stateId && c.CityID == cityId);
                        if (cityRow != null) le.City = cityRow.CityName;

                        var stateRow = states.FirstOrDefault(s => s.StateID == stateId);
                        if (stateRow != null) le.State = stateRow.StateName;

                        return le;
                    }).ToList();

                    // ---- Employees per office (fetch for each code shown on the page) ----
                    // We build a dictionary keyed by NORMALIZED LegalEntityCode
                    var employeesByCode = new Dictionary<string, List<CustomersEmployee>>(StringComparer.OrdinalIgnoreCase);

                    var codes = combined
                        .Select(le => le.LegalEntityCode)
                        .Where(c => !string.IsNullOrWhiteSpace(c))
                        .Select(Norm)
                        .Distinct(StringComparer.OrdinalIgnoreCase)
                        .ToList();

                    // 1. Fetch employees for every code
                    var rawEmployeesByCode = new Dictionary<string, List<CustomersEmployee>>(StringComparer.OrdinalIgnoreCase);

                    foreach (var code in codes)
                    {
                        try
                        {
                            var apiEmployeeUrl = $"{AppUrlConstant.GetCustomerEmployee}?LegalEntityCode={Uri.EscapeDataString(code)}";
                            var empResponse = await client.GetAsync(apiEmployeeUrl);

                            if (!empResponse.IsSuccessStatusCode)
                                continue;

                            var empJson = await empResponse.Content.ReadAsStringAsync();
                            var list = JsonConvert.DeserializeObject<List<CustomersEmployee>>(empJson) ?? new List<CustomersEmployee>();

                            list = list
                                .Where(x => x.AppStatus == 0)
                                .OrderBy(x => x.UserID)
                                .ToList();

                            rawEmployeesByCode[code] = list;
                        }
                        catch (Exception exEmp)
                        {
                            Console.WriteLine("Employee fetch error for code " + code + ": " + exEmp.Message);
                        }
                    }

                    // 2. Fix parent counts by subtracting child employees
                    //var employeesByCode = new Dictionary<string, List<CustomersEmployee>>(StringComparer.OrdinalIgnoreCase);

                    foreach (var office in combined)
                    {
                        var normCode = Norm(office.LegalEntityCode);
                        if (!rawEmployeesByCode.TryGetValue(normCode, out var list)) continue;

                        // Replace the following code block:  
                        // var employeesByCode = new Dictionary<string, List<CustomersEmployee>>(StringComparer.OrdinalIgnoreCase);  

                        // With this updated code:  
                        var employeesByCodeFinal = new Dictionary<string, List<CustomersEmployee>>(StringComparer.OrdinalIgnoreCase);

                        // Remove employees that actually belong to children
                        var childCodes = combined
                            .Where(c => c.ParentLegalEntityCode == office.LegalEntityCode)
                            .Select(c => Norm(c.LegalEntityCode))
                            .ToList();

                        var filtered = list.Where(emp => !childCodes.Contains(Norm(emp.LegalEntityCode))).ToList();

                        employeesByCode[normCode] = filtered;
                    }


                    // ---- ViewBags for the View ----
                    ViewBag.EmployeesByCode = employeesByCode;                           // For per-office counts

                    // Compact names JSON for the popup (FullName + Designation), keys already normalized
                    var mapForView = employeesByCode.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Select(e => new
                        {
                            FullName = $"{(e.FirstName ?? "").Trim()} {(e.LastName ?? "").Trim()}".Trim(),
                            e.Designation
                        }).ToList(),
                        StringComparer.OrdinalIgnoreCase
                    );
                    ViewBag.EmpNamesByCodeJson = JsonConvert.SerializeObject(mapForView);

                    // Optional: overall count (sum of all displayed offices)
                    ViewBag.EmployeeCount = employeesByCode.Values.Sum(v => v?.Count ?? 0);

                    ViewBag.LegalEntityCode = legalEntityCode;
                    ViewBag.LegalEntityName = legalEntityName;
                    ViewBag.StateMap = states.ToDictionary(s => s.StateID.ToString(), s => s.StateName);
                    ViewBag.CityMap = cities.ToDictionary(c => c.CityID.ToString(), c => c.CityName);
                    ViewBag.Id = IdLegal;

                    return View(combined);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ShowOrganization error: " + ex.Message);
                return View(new List<LegalEntity>());
            }
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
            //List<State> states = new();
            List<JetwaysAdmin.Entity.State> states = new();

            using (HttpClient client = new HttpClient())
            {
                string url = $"{AppUrlConstant.GetSate}/{CountryId}";
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    states = JsonConvert.DeserializeObject<List<JetwaysAdmin.Entity.State>>(json);
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


        [HttpGet]
       // public IActionResult GetWorkLocations(int legalEntityId)
       // {
       //// TODO: replace with your real data lookup
       //    ///  Example shape: new { stateID = 7, stateName = "Delhi" }
       //     var states = _yourRepo.GetStatesByLegalEntity(legalEntityId)
       //         .Select(s => new { stateID = s.StateID, stateName = s.StateName });

       //     return Json(states);
       // }

        [HttpPost]
        public async Task<IActionResult> AddOffice([FromForm] LegalEntity legalEntity,int IdLegal, string ParentLegalEntityCode, string ParentLegalEntityName)
        {
            ViewBag.LegalEntityCode = ParentLegalEntityCode;
            ViewBag.LegalEntityName = ParentLegalEntityName;
            ViewBag.Id = IdLegal;
            var file = Request.Form.Files.FirstOrDefault(f => f.Name == "Logo");
            if (file != null && file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    legalEntity.Logo = ms.ToArray();
                }
            }
            using (HttpClient client = new HttpClient())
            {
                var legalentityalldata = await client.GetAsync(AppUrlConstant.GetLegalEntity);
                if (legalentityalldata.IsSuccessStatusCode)
                {
                    var existingJson = await legalentityalldata.Content.ReadAsStringAsync();
                    var parsedResult = JsonConvert.DeserializeObject<LegalEntityResponse>(existingJson);
                    var existingData = parsedResult?.Data ?? new List<LegalEntity>();
                    //bool nameDup = existingData.Any(e =>
                    //string.Equals(e.LegalEntityName?.Trim(), legalEntity.LegalEntityName?.Trim(), StringComparison.OrdinalIgnoreCase));
                    bool codeDup = existingData.Any(e =>
                    string.Equals(e.LegalEntityCode?.Trim(), legalEntity.LegalEntityCode?.Trim(), StringComparison.OrdinalIgnoreCase));
                    bool corpDup = !string.IsNullOrWhiteSpace(legalEntity.CorporateAccountsCode) &&
                         existingData.Any(e =>
                             string.Equals(e.CorporateAccountsCode?.Trim(),
                                           legalEntity.CorporateAccountsCode?.Trim(),
                                           StringComparison.OrdinalIgnoreCase));

                    bool isDuplicate =  codeDup || corpDup;
                    if (isDuplicate)
                    {
                        TempData["DuplicateError"] = "Duplicate entry found! Please enter unique Code, or Corporate Account Code.";
                        return Json(new
                        {
                            success = false,
                            duplicate = true,
                            message = TempData["DuplicateError"] as string
                        });
                        //return RedirectToAction("ShowOrganization", new { LegalEntityCode = ParentLegalEntityCode, LegalEntityName = ParentLegalEntityName });
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

        [HttpPost]
        public async Task<IActionResult> UploadOffices(IFormFile file, int IdLegal, string ParentLegalEntityCode, string ParentLegalEntityName)
        {
            ViewBag.LegalEntityCode = ParentLegalEntityCode;
            ViewBag.LegalEntityName = ParentLegalEntityName;
            ViewBag.Id = IdLegal;

            if (file == null || file.Length == 0)
                return Json(new { success = false, message = "No file selected." });

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            stream.Position = 0;

            using var workbook = new XLWorkbook(stream);
            var worksheet = workbook.Worksheets.FirstOrDefault();
            if (worksheet == null)
                return Json(new { success = false, message = "No worksheet found in the uploaded file." });

            // --- Read Header Row (Row 1) and create header->column map ---
            var headerRow = worksheet.Row(1);
            var headerMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            foreach (var cell in headerRow.CellsUsed())
            {
                var header = cell.GetString()?.Trim();
                if (!string.IsNullOrWhiteSpace(header) && !headerMap.ContainsKey(header))
                    headerMap[header] = cell.Address.ColumnNumber;
            }

            // --- Required headers (add more if you want) ---
            var requiredHeaders = new[] { "LegalEntityName" };
            var missingHeaders = requiredHeaders.Where(h => !headerMap.ContainsKey(h)).ToList();
            if (missingHeaders.Any())
            {
                return Json(new
                {
                    success = false,
                    message = "Missing required header(s): " + string.Join(", ", missingHeaders)
                });
            }

            // Helpers: safely read by header name (missing header => null)
            string GetString(int row, string header)
            {
                if (!headerMap.TryGetValue(header, out int col)) return null;
                return worksheet.Cell(row, col).GetString().Trim();
            }

            int? GetIntNullable(int row, string header)
            {
                var s = GetString(row, header);
                return int.TryParse(s, out var v) ? v : (int?)null;
            }

            bool? GetBoolNullable(int row, string header)
            {
                var s = GetString(row, header);
                if (string.IsNullOrWhiteSpace(s)) return null;

                s = s.Trim();

                // Support: true/false, 1/0, yes/no
                if (bool.TryParse(s, out var b)) return b;
                if (s == "1") return true;
                if (s == "0") return false;
                if (s.Equals("yes", StringComparison.OrdinalIgnoreCase)) return true;
                if (s.Equals("no", StringComparison.OrdinalIgnoreCase)) return false;

                return null;
            }

            DateTime? GetDateNullable(int row, string header)
            {
                var s = GetString(row,  header);
                return DateTime.TryParse(s, out var d) ? d : (DateTime?)null;
            }

            var rowCount = worksheet.LastRowUsed()?.RowNumber() ?? 1;

            using var client = new HttpClient();

            for (int row = 2; row <= rowCount; row++)
            {
                // Required field
                var legalEntityName = GetString(row, "LegalEntityName");
                if (string.IsNullOrWhiteSpace(legalEntityName))
                    continue; // skip blank rows

                // Read values (if header not in Excel => null)
                var id = GetIntNullable(row, "Id") ?? 0;
                var officeType = GetString(row, "OfficeType");
                var legalEntityCode = GetString(row, "LegalEntityCode");
                var financialType = GetString(row, "FinancialType");

                // ParentLegalEntityCode: take from Excel if present, else use parameter
                var parentCodeFromExcel = GetString(row, "ParentLegalEntityCode");
                var finalParentCode = !string.IsNullOrWhiteSpace(parentCodeFromExcel) ? parentCodeFromExcel : ParentLegalEntityCode;

                var gstLocationId = GetIntNullable(row, "GSTLocationID");
                var financialPersonalCode = GetString(row, "FinancialPersonalCode");
                var assignIataGroup = GetString(row, "AssignIATAGroup");
                var corporateAccountsCode = GetString(row, "CorporateAccountsCode");

                var firstName = GetString(row, "FirstName");   // may not exist -> null
                var lastName = GetString(row, "LastName");     // may not exist -> null
                var businessEmail = GetString(row, "BussinesEmail");
                var password = GetString(row, "Password");
                var mobileNumber = GetString(row, "MobileNumber");
                var gender = GetString(row, "Gender");
                var addressLine1 = GetString(row, "AddressLine1");
                var addressLine2 = GetString(row, "AddressLine2");
                var country = GetString(row, "Country");
                var state = GetString(row, "State");
                var city = GetString(row, "City");
                var postalCode = GetString(row, "PostalCode");

                var accountType = GetString(row, "AccountType");
                var integrationRefNumber = GetString(row, "IntegrationRefNumber");
                var gstApplicable = GetBoolNullable(row, "GSTApplicableManagementFee");
                var passGSTDetailsAirline = GetBoolNullable(row, "PassGSTDetailsAirline");
                var isSEZ = GetBoolNullable(row, "IsSEZ");
                var customerBaseCurrency = GetString(row, "CustomerBaseCurrency");
                var customerBaseCountry = GetString(row, "CustomerBaseCountry");
                var travelDeskEmail = GetString(row, "TravelDeskEmail");

                var accountActivationDate = GetDateNullable(row, "AcountActivationDate");
                var accountDeactivationDate = GetDateNullable(row, "AccountDeactivationDate");

                var createNewGds = GetBoolNullable(row, "CreateNewGDSProfile");
                var updateExistingCustomer = GetBoolNullable(row, "UpdateExistingCustomerProfile");

                var createdBy = GetString(row, "CreatedBy");
                var createdDate = GetDateNullable(row, "CreatedDate");
                var modifyBy = GetString(row, "ModifyBy");
                var modifyDate = GetDateNullable(row, "ModifyDate");

                var appStatus = GetIntNullable(row, "AppStatus");

                var legalEntity = new LegalEntity
                {
                    Id = id,
                    OfficeType = officeType,
                    LegalEntityName = legalEntityName,
                    LegalEntityCode = string.IsNullOrWhiteSpace(legalEntityCode) ? Guid.NewGuid().ToString("N")[..8] : legalEntityCode,
                    FinancialType = financialType,
                    ParentLegalEntityCode = finalParentCode,
                    GSTLocationID = gstLocationId,
                    FinancialPersonalCode = financialPersonalCode,
                    AssignIATAGroup = assignIataGroup,
                    CorporateAccountsCode = corporateAccountsCode,
                    FirstName = firstName,
                    LastName = lastName,
                    BussinesEmail = businessEmail,
                    Password = password,
                    MobileNumber = mobileNumber,
                    Gender = gender,
                    AddressLine1 = addressLine1,
                    AddressLine2 = addressLine2,
                    Country = country,
                    State = state,
                    City = city,
                    PostalCode = postalCode,
                    AccountType = accountType,
                    IntegrationRefNumber = integrationRefNumber,
                    GSTApplicableManagementFee = gstApplicable,
                    PassGSTDetailsAirline = passGSTDetailsAirline,
                    IsSEZ = isSEZ,
                    CustomerBaseCurrency = customerBaseCurrency,
                    CustomerBaseCountry = customerBaseCountry,
                    TravelDeskEmail = travelDeskEmail,
                    AcountActivationDate = accountActivationDate,
                    AccountDeactivationDate = accountDeactivationDate,
                    CreateNewGDSProfile = createNewGds,
                    UpdateExistingCustomerProfile = updateExistingCustomer,
                    Createdby = string.IsNullOrWhiteSpace(createdBy) ? "Admin" : createdBy,
                    CreatedDate = createdDate,
                    ModifyBy = string.IsNullOrWhiteSpace(modifyBy) ? "Admin2" : modifyBy,
                    ModifyDate = modifyDate,
                    AppStatus = appStatus
                };

                var response = await client.PostAsJsonAsync(AppUrlConstant.AddLegalEntity, legalEntity);

                if (!response.IsSuccessStatusCode)
                {
                    return Json(new
                    {
                        success = false,
                        message = $"Error inserting record '{legalEntityName}' at row {row}."
                    });
                }
            }

            TempData["UploadFile"] = "Data inserted successfully.";
            return RedirectToAction("ShowOrganization", new { LegalEntityCode = ParentLegalEntityCode, LegalEntityName = ParentLegalEntityName });
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
              
            }
            
           ViewBag.LegalEntitySubCode = entity.LegalEntityCode;
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
            var file = Request.Form.Files.FirstOrDefault(f => f.Name == "Logo");
            if (file != null && file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    legalEntity.Logo = ms.ToArray();
                }
            }
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
       // [ServiceFilter(typeof(LogActionFilter))]
        public async Task<IActionResult> ManageStaff([FromForm] CustomerManageStaff customermanagestaff, int IdLegal, string LegalEntityCodeParent, string LegalEntityName, string UpdateManageStaff)
        {
           
            ViewBag.LegalEntityCode = LegalEntityCodeParent;
            ViewBag.LegalEntityName = LegalEntityName;
            ViewBag.Id = IdLegal;
            string employeeData = customermanagestaff.BookingConsultant ?? string.Empty;
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
                string LegalEntityCode = LegalEntityCodeParent;
                if (UpdateManageStaff == "UpdateStaff")
                {
                    return RedirectToAction("ShowOrganization", new
                    {
                        IdLegal,
                        LegalEntityCode,
                        LegalEntityName
                    });
                }
                else
                {
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