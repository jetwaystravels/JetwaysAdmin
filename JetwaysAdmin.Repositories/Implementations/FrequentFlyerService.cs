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
    public class FrequentFlyerService: IFrequentFlyer<EmployeeFrequentFlyer>
    {
        private readonly AppDbContext _context;

        public FrequentFlyerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddFrequentFlyer(EmployeeFrequentFlyer employeefrequentFlyer)
        {
            await _context.tb_EmployeeFrequentFlyers.AddAsync(employeefrequentFlyer);
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<EmployeeFrequentFlyer>> GetAllFrequentFlyer()
        {
            return await _context.tb_EmployeeFrequentFlyers.ToListAsync();
        }

        public async Task<EmployeeFrequentFlyer> GetFrequentFlyerById(int id)
        {
            return await _context.tb_EmployeeFrequentFlyers.FirstOrDefaultAsync(e => e.FrequentFlyerID == id);
        }


        public async Task UpdateFrequentFlyer(EmployeeFrequentFlyer frequentFlyer)
        {
            _context.tb_EmployeeFrequentFlyers.Update(frequentFlyer);
            await _context.SaveChangesAsync();
        }


    }
}
