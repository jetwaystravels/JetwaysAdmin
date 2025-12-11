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
    public class CustomerBandService : ICustomerBand<CustomerBand>
    {
        private readonly AppDbContext _context;

        public CustomerBandService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CustomerBand>> GetAllBand()
        {
            
            return await _context.tb_CustomerBand.ToListAsync();
        }
        public async Task AddCustomerBand(CustomerBand customerband)
        {
            await _context.tb_CustomerBand.AddAsync(customerband);
            await _context.SaveChangesAsync();
        }

    }
}
