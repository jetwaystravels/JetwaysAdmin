using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Repositories.Implementations
{
    public class SuppliersCredentialService: ISuppliersCredential<SuppliersCredential>
    {
        private readonly AppDbContext _context;
        public SuppliersCredentialService(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddSupplierCredential(SuppliersCredential supplierscredential)
        {
            await _context.tb_SuppliersCredential.AddAsync(supplierscredential);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<SuppliersCredential>> GetSupplierCredential()
        {
            return await _context.tb_SuppliersCredential.ToListAsync();
        }
        public async Task<SuppliersCredential> GetSupplierCredentialById(int Id)
        {
            return await _context.tb_SuppliersCredential.FirstOrDefaultAsync(e => e.Id == Id);
        }
        public async Task UpdateSupplierCredentialById(SuppliersCredential supplierscredential)
        {
            _context.tb_SuppliersCredential.Update(supplierscredential);
            await _context.SaveChangesAsync();
        }
    }
}
