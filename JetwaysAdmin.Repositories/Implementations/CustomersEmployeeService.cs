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
    public class CustomersEmployeeService : ICustomersEmployee<CustomersEmployee>
    {
        private readonly AppDbContext _context;

        public CustomersEmployeeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomersEmployee>> GetAllCustomerEmployee()
        {
            return await _context.tb_CustomersEmployee.ToListAsync();

        }
        public async Task<IEnumerable<CustomersEmployee>> GetCustomerEmployeeByLegalEntity(string legalEntityCode)
        {
            return await _context.tb_CustomersEmployee
                .FromSqlRaw("EXEC GetCustomerEmployeesByLegalEntity @LegalEntityCode = {0}", legalEntityCode)
                .ToListAsync();
        }

        public async Task<CustomersEmployee?> GetUsersById(int id)
        {
            var param = new SqlParameter("@UserID", id);

            var rows = await _context.tb_CustomersEmployee
                .FromSqlRaw("EXEC sp_CustomersEmployee_GetById @UserID", param)
                .AsNoTracking()
                .ToListAsync();

            return rows.FirstOrDefault();
        }

        public async Task Addusers(CustomersEmployee UserAdd)
        {
            await _context.tb_CustomersEmployee.AddAsync(UserAdd);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUsersById(CustomersEmployee Users)
        {
            _context.tb_CustomersEmployee.Update(Users);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePasswordAsync(int userId, string passwordHash)
        {
            var user = await _context.tb_CustomersEmployee.FirstOrDefaultAsync(x => x.UserID == userId);
            if (user == null) return false;

            // adjust property name to your schema
            user.Password = passwordHash;
            user.ModifyDate = DateTime.UtcNow;   // optional audit field if you have one

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateStatusAsync(int id, int status, string legalEntityCode)
        {
            try
            {
                var employee = await _context.tb_CustomersEmployee
                    .FirstOrDefaultAsync(e => e.UserID == id && e.LegalEntityCode == legalEntityCode);

                if (employee == null)
                    return false; // not found

                employee.AppStatus = status;
                employee.ModifyBy = "Admin"; // or from logged-in user
                employee.ModifyDate = DateTime.Now;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // log exception if needed
                return false;
            }
        }
    }
}
