using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Repositories.Implementations
{
    public class LegalEntityDBService : ILegalEntityDB<LegalEntityDB>
    {
        private readonly AppDbContext _context;

        public LegalEntityDBService(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddLegalEntityDB(LegalEntityDB legalEntity)
        {
            await _context.Admin_tb_LegalEntityDB.AddAsync(legalEntity);
            await _context.SaveChangesAsync();
        }
    }
}
