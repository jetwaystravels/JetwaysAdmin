using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace JetwaysAdmin.Repositories.Implementations
{
    public class BillingEntityService : IBillingEntity<BillingEntity>
    {
        private readonly AppDbContext _context;

        public BillingEntityService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BillingEntity>> GetBillingEntityAsync(string LegalEntityCode, string EmployeeCode)
        {
            var entityParam = new SqlParameter("@LegalEntityCode", LegalEntityCode ?? (object)DBNull.Value);
            var empParam = new SqlParameter("@EmployeeCode", EmployeeCode ?? (object)DBNull.Value);
           

            return await _context
                .Set<BillingEntity>()
                .FromSqlRaw("EXEC GetBillingEntityByLegalEntityAndUser @LegalEntityCode, @EmployeeCode", entityParam, empParam)
                .ToListAsync();
        }
    }
   
}

