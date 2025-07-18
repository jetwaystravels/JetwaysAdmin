using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Repositories.Implementations
{
    public class ManageStaffService : IManageStaff<CustomerManageStaff>
    {
        private readonly AppDbContext _customermanagestaff;
     

        public ManageStaffService(AppDbContext customermanagestaff)
        {
            _customermanagestaff = customermanagestaff;
           
        }

        //public async Task ManageStaff(CustomerManageStaff customermanagestaff)
        //{
        //    await _customermanagestaff.tb_CustomerManageStaff.AddAsync(customermanagestaff);
        //    await _customermanagestaff.SaveChangesAsync();  
        //}


        public async Task ManageStaff(CustomerManageStaff customermanagestaff)
        {
            var parameters = new[]
            {
                new SqlParameter("@LegalEntityCode", customermanagestaff.LegalEntityCode ?? (object)DBNull.Value),
                new SqlParameter("@key_account_manager", (object?)customermanagestaff.KeyAccountManager ?? DBNull.Value),
                new SqlParameter("@Sales_spoc", (object?)customermanagestaff.SalesSpoc ?? DBNull.Value),
                new SqlParameter("@Booking_consultant", (object?)customermanagestaff.BookingConsultant ?? DBNull.Value),
                new SqlParameter("@Emergency_contact", (object?)customermanagestaff.EmergencyContact ?? DBNull.Value),
                new SqlParameter("@User_group", (object?)customermanagestaff.UserGroup ?? DBNull.Value)
            };

            await _customermanagestaff.Database.ExecuteSqlRawAsync(
                "EXEC sp_AddOrUpdateCustomerManageStaff @LegalEntityCode, @key_account_manager, @Sales_spoc, @Booking_consultant, @Emergency_contact, @User_group",
                parameters
            );
        }



        public async Task<BookingConsultantDto?> GetBookingConsultantsAsync(string legalEntityCode)
        {
            var result = await _customermanagestaff
            .BookingConsultants
            .FromSqlInterpolated($"EXEC GetBookingConsultantsByEntityCode @LegalEntityCode = {legalEntityCode}")
            .ToListAsync();

            return result.FirstOrDefault();
        }
    }
}
