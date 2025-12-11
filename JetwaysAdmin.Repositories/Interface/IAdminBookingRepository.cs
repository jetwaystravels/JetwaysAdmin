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
        /// <summary>
        /// Get list of bookings (selected columns only) using stored procedure.
        /// </summary>
        Task<IEnumerable<BookingListDto>> GetAllAsync();

        /// <summary>
        /// Get one booking (selected columns only) using stored procedure.
        /// </summary>
        Task<BookingListDto?> GetByBookingIdAsync(string bookingId);

        // 🔥 New method with filters + pagination
        Task<PagedResult<BookingListDto>> GetFilteredAsync(BookingFilter filter);
    }
}
