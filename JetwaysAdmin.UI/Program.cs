using JetwaysAdmin.UI.Controllers.CustomeFilter;
using JetwaysAdmin.UI.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;

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

// ===== Normal Cookie Authentication (NO Entra ID) =====
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/UserLogin";
        options.LogoutPath = "/Login/Logout";
        options.AccessDeniedPath = "/Login/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    });

builder.Services.AddAuthorization();

// Bind AccessControl
builder.Services.Configure<AccessControlSettings>(
    builder.Configuration.GetSection("AccessControl"));

// Data Protection API
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(@"C:\DataProtectionKeys"))
    .SetApplicationName("JetwaysAdmin");

builder.Services.AddScoped<EncryptionService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=UserLogin}/{id?}");

app.Run();
