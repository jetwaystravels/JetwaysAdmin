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
    public class CompanyEmployeeGSTService : ICompanyEmployeeGST<CompanyEmployeeGSTDetails>
    {

        private readonly AppDbContext _context;
        public CompanyEmployeeGSTService(AppDbContext context)
        {
            _context = context;
        }
      

        public async Task<IEnumerable<CompanyEmployeeGSTDetails>> GetCompanyEmployeeGstAsync(string EmployeeCode, string LegalEntityCode)
        {
            var empParam = new SqlParameter("@EmployeeCode", EmployeeCode ?? (object)DBNull.Value);
            var entityParam = new SqlParameter("@LegalEntityCode", LegalEntityCode ?? (object)DBNull.Value);

            return await _context
                .Set<CompanyEmployeeGSTDetails>()
                .FromSqlRaw("EXEC sproc_GetCompanyEmployeeGSTDetails @EmployeeCode, @LegalEntityCode", empParam, entityParam)
                .ToListAsync();
        }
    }
}
