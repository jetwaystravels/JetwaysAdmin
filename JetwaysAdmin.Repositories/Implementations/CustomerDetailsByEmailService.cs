using System;
using System.Collections;
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
    public class CustomerDetailsByEmailService
     : ICustomerDetailsByEmail<CustomerDetails, CustomerDealCodes>
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

        public async Task<IEnumerable<CustomerDealCodes>> GetCustomerdealCodeAsync(string legalEntityCode, string iata)
        {

            ////var legalEntityParam = new SqlParameter("@LegalEntityCode", legalEntityCode);
            ////var iataParam = new SqlParameter("@IATAGroup", Convert.ToInt32(iata));

            //var result = await _context.CustomerDealCodes
            //    .Where(x => x.LegalEntityCode == legalEntityCode && x.IATAGroup == Convert.ToInt32(iata))
            //    .ToListAsync();

            //return result;


            var legalEntityParam = new SqlParameter("@LegalEntityCode", legalEntityCode);
            var iataParam = new SqlParameter("@IATAGroup", Convert.ToInt32(iata));

            var result = await _context.CustomerDealCodes
                .FromSqlRaw("EXEC GetCustomerDealCodes @LegalEntityCode, @IATAGroup",
                            legalEntityParam, iataParam)
                .ToListAsync();

            return result;
        }
    }
}
