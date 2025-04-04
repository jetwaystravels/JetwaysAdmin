using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace JetwaysAdmin.Repositories.Implementations
{
    public class CustomerDetailsByEmailService:ICustomerDetailsByEmail<CustomerDetails>
    {

        private readonly AppDbContext _context;
        public CustomerDetailsByEmailService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomerDetails>> GetCustomerDetailsByEmailAsync(string email)
        {
            var emailParam = new SqlParameter("@AdminEmail", email);

            return await _context.CustomerDetails
                .FromSqlRaw("EXEC sproc_GetCustomerDetailsByEmail @AdminEmail", emailParam)
                .ToListAsync();
        }

    }
}
