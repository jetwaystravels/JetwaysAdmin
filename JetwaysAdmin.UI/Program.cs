using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Implementations;
using JetwaysAdmin.Repositories.Interface;
using JetwaysAdmin.UI.Controllers.CustomeFilter;
using JetwaysAdmin.UI.Models;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Filters / DI
builder.Services.AddScoped<LogActionFilter>();

// ==== Entra ID (Azure AD) Authentication ====
builder.Services
    .AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddAuthorization(options =>
{
    // Example: protect by an Entra group (Object ID of the group)
    // Add the group claim in Azure Portal -> App registrations -> Token configuration -> "Add groups claim".
    options.AddPolicy("HRGroupOnly", p =>
        p.RequireClaim("groups", "YOUR_AAD_GROUP_OBJECT_ID"));
});

// Optional: adds default UI endpoints like /MicrosoftIdentity/Account/SignIn
builder.Services.AddRazorPages().AddMicrosoftIdentityUI();
builder.Services.Configure<AccessControlSettings>(
    builder.Configuration.GetSection("AccessControl"));
var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();          // session before auth is fine; also OK after auth
app.UseAuthentication();   // <<< REQUIRED for Entra sign-in
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=UserLogin}/{id?}");

app.Run();
