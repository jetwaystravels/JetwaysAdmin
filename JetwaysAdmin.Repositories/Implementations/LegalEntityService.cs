using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace JetwaysAdmin.Repositories.Implementations
{
    public class LegalEntityService : ILegalEntity<LegalEntity>
    {

        private readonly AppDbContext _context;

        public LegalEntityService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LegalEntity>> GetAllLegalEntity()
        {
            return await _context.Admin_tb_LegalEntity.ToListAsync();
           
        }

        public async Task<LegalEntity> GetLegalEntityById(int id)
        {
            return await _context.Admin_tb_LegalEntity.FindAsync();
        }
        public async Task AddLegalEntity(LegalEntity legalEntity)
        {
            await _context.Admin_tb_LegalEntity.AddAsync(legalEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLegalEntity(int id)
        {
            var legalEntity = await _context.Admin_tb_LegalEntity.FindAsync(id);
            if (legalEntity != null)
            {
                _context.Admin_tb_LegalEntity.Remove(legalEntity);
                await _context.SaveChangesAsync();
            }
        }
                

        public  async Task UpdateLegalEntity(LegalEntity legalEntity)
        {
            _context.Admin_tb_LegalEntity.Update(legalEntity);
             await _context.SaveChangesAsync();
        }
    }
}
