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
    public class DealCodeService : IDealCode<DealCode>
    {
        private readonly AppDbContext _dealCodeRepository;
        public DealCodeService(AppDbContext dealCodeRepository)
        {
            _dealCodeRepository = dealCodeRepository;
        }
        public async Task AddDealCode(DealCode dealCode)
        {
            await _dealCodeRepository.tb_DealCode.AddAsync(dealCode);
            await _dealCodeRepository.SaveChangesAsync();
        }
        public async Task<IEnumerable<DealCode>> GetDealCode()
        {
            return await _dealCodeRepository.tb_DealCode.ToListAsync();
        }
        public async Task<DealCode> GetDealCodeById(int DealCodeId)
        {
            return await _dealCodeRepository.tb_DealCode.FirstOrDefaultAsync(e => e.DealCodeId == DealCodeId);
        }
        public async Task UpdateDealCodeById(DealCode dealCode)
        {
            _dealCodeRepository.tb_DealCode.Update(dealCode);
            await _dealCodeRepository.SaveChangesAsync();
        }
    }
}
