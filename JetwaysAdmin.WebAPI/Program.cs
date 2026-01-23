using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories;
using JetwaysAdmin.Repositories.Implementations;
using JetwaysAdmin.Repositories.Interface;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("AdminConnection")));
builder.Services.AddDbContext<CoreDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CoreConnection")));



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAdmin<Admin>, AdminService>();
builder.Services.AddScoped<AdminService>();
builder.Services.AddScoped<IMenu<MenuItem>, MenuService>();
builder.Services.AddScoped<ICustomer<Customer>, CustomerService>();
builder.Services.AddScoped<ILegalEntity<LegalEntity>, LegalEntityService>();
builder.Services.AddScoped<ILegalEntityDB<LegalEntityDB>, LegalEntityDBService>();
builder.Services.AddScoped<ICustomerDetailsByEmail<CustomerDetails,CustomerDealCodes>, CustomerDetailsByEmailService>();
builder.Services.AddScoped<ICompanyEmployeeGST<CompanyEmployeeGSTDetails>, CompanyEmployeeGSTService>();
builder.Services.AddScoped<IBillingEntity<BillingEntity>, BillingEntityService>();
builder.Services.AddScoped<IHierarchyLegalEntity<HierarchyLegalEntity>, HierarchicallegalentityService>();
builder.Services.AddScoped<IIATAGroup<IATAGroup>,IATAGruopService>();
builder.Services.AddScoped<IAccountDetails<AccountDetails>, AccountDetailsService>();
builder.Services.AddScoped<IAddUser<AddUser>, AddUserService>();
builder.Services.AddScoped<ICustomerAccountBalance<CustomerAccountBalance>, AccountBalanceService>();
builder.Services.AddScoped<IManageStaff<CustomerManageStaff>, ManageStaffService>();
builder.Services.AddScoped<IDashboard<DashboardSummary>, DashboardService>();
builder.Services.AddScoped<IAddNewSupplier<AddSupplier>, AddNewSupplierService>();

builder.Services.AddScoped<ICustomersEmployee<CustomersEmployee>, CustomersEmployeeService>();  
builder.Services.AddScoped<IFrequentFlyer<EmployeeFrequentFlyer>, FrequentFlyerService>();  
builder.Services.AddScoped<IInternalUsers<InternalUsers>, InternalUsersService>();  
builder.Services.AddScoped<IContactUsDetails<ContactUsDetails>, ContactUsDetailsService>();
builder.Services.AddScoped<IEmployeeBillingEntity<EmployeeBillingEntity>, EmployeeBillingEntityService>();
builder.Services.AddScoped<ILocationsandTax<LocationsandTax>, LocationsandTaxService>();
builder.Services.AddScoped<ILocation<AddressCountryState>, LocationService>();
builder.Services.AddScoped<ISuppliersCredential<SuppliersCredential>, SuppliersCredentialService>();
builder.Services.AddScoped<IDealCode<DealCode>, DealCodeService>();
builder.Services.AddScoped<ICustomerDepartment<CustomerDepartmentData>, CustomerDepartmentService>();
builder.Services.AddScoped<ICustomerDesignation<CustomerDesignation>, CustomerDesignationService>();
builder.Services.AddScoped<ICustomerBand<CustomerBand>, CustomerBandService>();
builder.Services.AddScoped<IMenuRightsRepository, MenuRightsService>();
builder.Services.AddScoped<IAdminBookingRepository, AdminBookingService>();
builder.Services.AddScoped<IPassword, PasswordService>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(@"C:\DataProtectionKeys"))
    .SetApplicationName("JetwaysAdmin");

builder.Services.AddScoped<EncryptionService>();

var app = builder.Build();
//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
