using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Implementations;
using JetwaysAdmin.Repositories.Interface;
using JetwaysAdmin.UI.Controllers.CustomeFilter;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddScoped<LogActionFilter>();
//For globally use
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<LogActionFilter>();
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromHours(10);
                options.LoginPath = "/Login/UserLogin";
                //o.Cookie.Name = options.CookieName;
                //o.Cookie.Domain = options.CookieDomain;
                //o.SlidingExpiration = true;
                //o.ExpireTimeSpan = options.CookieLifetime;
                //o.TicketDataFormat = ticketFormat;
                //o.CookieManager = new CustomChunkingCookieManager();
            });

builder.Services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0).AddSessionStateTempDataProvider();
builder.Services.AddSession();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=UserLogin}/{id?}");

app.Run();
