using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
namespace JetwaysAdmin.Repositories.Implementations
{
    public class HierarchicallegalentityService : IHierarchyLegalEntity<HierarchyLegalEntity>
    {
        private readonly AppDbContext _context;

        public HierarchicallegalentityService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HierarchyLegalEntity>> GetHierarchicallegalentityAsync(string legalEntityCode)
        {
            var legalEntityParam = new SqlParameter("@LegalEntityCode", legalEntityCode ?? (object)DBNull.Value);
            return await _context
                .Set<HierarchyLegalEntity>()
                .FromSqlRaw("EXEC sproc_GetHierarchicallegalentity @LegalEntityCode", legalEntityParam)
                .ToListAsync();
        }


    }
}
