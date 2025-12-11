using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace JetwaysAdmin.Repositories.Implementations
{
    public class AdminBookingService : IAdminBookingRepository
    {
        private readonly AppDbContext _context;

        public AdminBookingService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AdminBooking?> GetByBookingIdAsync(string bookingId)
        {
            return await _context.AdminBookings
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.BookingID == bookingId);
        }

        public async Task<IEnumerable<AdminBooking>> GetByRecordLocatorAsync(string recordLocator)
        {
            return await _context.AdminBookings
                .AsNoTracking()
                .Where(b => b.RecordLocator == recordLocator)
                .ToListAsync();
        }

        public async Task<IEnumerable<AdminBooking>> GetAllAsync()
        {
            return await _context.AdminBookings
                .AsNoTracking()
                .OrderByDescending(b => b.BookedDate)
                .ToListAsync();
        }

        public async Task AddAsync(AdminBooking booking)
        {
            await _context.AdminBookings.AddAsync(booking);
        }

        public Task UpdateAsync(AdminBooking booking)
        {
            _context.AdminBookings.Update(booking);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
