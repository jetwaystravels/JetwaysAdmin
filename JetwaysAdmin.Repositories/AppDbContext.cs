using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Repositories
{
    public class AppDbContext: IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Admin> tb_admin { get; set; }
        public DbSet<MenuItem> tb_Menu { get; set; }
        public DbSet<MenuHead> Tb_Menuhead { get; set; }
        public DbSet<Customer> Admin_tb_Customers { get; set; }
        public DbSet<LegalEntity> Admin_tb_LegalEntity { get; set; }
        public DbSet<CustomerDetails> CustomerDetails { get; set; }
        public DbSet<IATAGroup> tb_IATAGroup { get; set; }
        public DbSet<AccountDetails> tb_CustomersAccountDetail { get; set; }
        public DbSet<AddUser> tb_AddNewUser { get; set; }
        public DbSet<CustomerAccountBalance> tb_CustomerAccountBalance { get; set; }
        public DbSet<CustomerManageStaff> tb_CustomerManageStaff { get; set; }
        public DbSet<DashboardSummary> DashboardSummary { get; set; }
        public DbSet<CompanyEmployeeGSTDetails> CompanyEmployeeGSTDetails { get; set; }
        public DbSet<HierarchyLegalEntity> hierarchyLegalEntity { get; set; }
        public DbSet<AddSupplier> tb_SuppliersDetail { get; set; }
        public DbSet<CustomersEmployee> tb_CustomersEmployee { get; set; }
        public DbSet<EmployeeFrequentFlyer> tb_EmployeeFrequentFlyers { get; set; }
        public DbSet<InternalUsers> tb_InternalUsers { get; set; }
        public DbSet<ContactUsDetails> tb_ContactUsDetails { get; set; }
        public DbSet<EmployeeBillingEntity> tb_EmployeeBillingEntity { get; set; }
        public DbSet<LocationsandTax> tb_CustomerLocationTaxDetails { get; set; }
        public DbSet<Country> tb_Country { get; set; }
        public DbSet<State> tb_State { get; set; }
        public DbSet<City> tb_City { get; set; }
        public DbSet<SuppliersCredential> tb_SuppliersCredential { get; set; }
        public DbSet<DealCode> tb_DealCode { get; set; }
        public DbSet<BookingConsultantDto> BookingConsultants { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerDetails>().HasNoKey(); // No primary key because it's a model
            modelBuilder.Entity<CompanyEmployeeGSTDetails>().HasNoKey();
            modelBuilder.Entity<BillingEntity>().HasNoKey();
            modelBuilder.Entity<HierarchyLegalEntity>().HasNoKey();
            modelBuilder.Entity<BookingConsultantDto>().HasNoKey();
            modelBuilder.Entity<Menu>()

       .HasOne(m => m.MenuHead)                 // navigation property in Menu (submenu)
       .WithMany(h => h.SubMenus)               // navigation collection in MenuHead
       .HasForeignKey(m => m.ParentId)          // FK in Menu table
       .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);
        }
    }
}
