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
    public class CustomerDesignationService : ICustomerDesignation<CustomerDesignation>
    {
        private readonly AppDbContext _context;

        public CustomerDesignationService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CustomerDesignation>> GetAllCustomerDesignation(string legalEntityCode)
        {
            return await _context.tb_CustomerDesignation.Where(emp => emp.LegalEntityCode == legalEntityCode)
                .ToListAsync();

        }
        public async Task AddCustomerDesignation(CustomerDesignation department)
        {
            await _context.tb_CustomerDesignation.AddAsync(department);
            await _context.SaveChangesAsync();
        }
    }
}
