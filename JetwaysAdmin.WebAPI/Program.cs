using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories;
using JetwaysAdmin.Repositories.Implementations;
using JetwaysAdmin.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));




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
builder.Services.AddScoped<ICustomerDetailsByEmail<CustomerDetails>, CustomerDetailsByEmailService>();


builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
