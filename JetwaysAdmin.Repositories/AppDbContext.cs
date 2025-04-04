using JetwaysAdmin.Entity;
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

        public DbSet<Customer> Admin_tb_Customers { get; set; }
        public DbSet<LegalEntity> Admin_tb_LegalEntity { get; set; }
    }
}
