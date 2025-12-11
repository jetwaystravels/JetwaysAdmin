using JetwaysAdmin.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Repositories.Interface
{
    public interface IAdminBookingRepository
    {
        Task<AdminBooking?> GetByBookingIdAsync(string bookingId);
        Task<IEnumerable<AdminBooking>> GetByRecordLocatorAsync(string recordLocator);
        Task<IEnumerable<AdminBooking>> GetAllAsync();
        Task AddAsync(AdminBooking booking);
        Task UpdateAsync(AdminBooking booking);
        Task SaveChangesAsync();
    }
}
