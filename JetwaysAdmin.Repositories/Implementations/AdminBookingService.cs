using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace JetwaysAdmin.Repositories.Implementations
{
    public class AdminBookingService : IAdminBookingRepository
    {
        private readonly CoreDbContext _context;

        public AdminBookingService(CoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookingListDto>> GetAllAsync()
        {
            return await _context.BookingListDtos
                .FromSqlRaw("EXEC sp_GetBookingList")
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<BookingListDto?> GetByBookingIdAsync(string bookingId)
        {
            var result = await _context.BookingListDtos
                .FromSqlRaw("EXEC sp_GetBookingByID @BookingID = {0}", bookingId)
                .AsNoTracking()
                .ToListAsync();

            return result.FirstOrDefault();
        }

        public async Task<PagedResult<BookingListDto>> GetFilteredAsync(BookingFilter filter)
        {
            // EF FromSqlRaw with positional placeholders
            var items = await _context.BookingListDtos
                .FromSqlRaw(
                    @"EXEC sp_GetBookingListFiltered 
                    @FromDate      = {0},
                    @ToDate        = {1},
                    @RecordLocator = {2},
                    @CompanyName   = {3},
                    @Consultant    = {4},
                    @PageNumber    = {5},
                    @PageSize      = {6}",
                    filter.FromDate,
                    filter.ToDate,
                    filter.RecordLocator,
                    filter.CompanyName,
                    filter.Consultant,
                    filter.PageNumber,
                    filter.PageSize)
                .AsNoTracking()
                .ToListAsync();

            return new PagedResult<BookingListDto>
            {
                Items = items,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize
            };
        }

    }
}
