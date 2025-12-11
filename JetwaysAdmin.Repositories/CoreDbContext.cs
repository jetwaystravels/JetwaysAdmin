using JetwaysAdmin.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Repositories
{
    public class CoreDbContext : DbContext
    {
        public CoreDbContext(DbContextOptions<CoreDbContext> options)
            : base(options)
        {
        }

        //public DbSet<AdminBooking> AdminBookings { get; set; }

        public DbSet<BookingListDto> BookingListDtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Keyless DTO mapped to stored procedure result
            modelBuilder.Entity<BookingListDto>(entity =>
            {
                entity.HasNoKey();
                entity.ToView(null); // no backing table or view, used only for FromSqlRaw

                // Optional: configure some column sizes to match DB
                // entity.Property(e => e.BookingID).HasMaxLength(100);
                // entity.Property(e => e.RecordLocator).HasMaxLength(50);
                // etc...
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
