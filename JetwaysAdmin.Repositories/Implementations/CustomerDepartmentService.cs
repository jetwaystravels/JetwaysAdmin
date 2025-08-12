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
    public class CustomerDepartmentService : ICustomerDepartment<CustomerDepartmentData>
    {
        private readonly AppDbContext _context;

        public CustomerDepartmentService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CustomerDepartmentData>> GetAllCustomerDepartment(string legalEntityCode)
        {
            return await _context.tb_CustomerDepartment.Where(emp => emp.LegalEntityCode == legalEntityCode)
                .ToListAsync();

        }
       
        public async Task AddCustomerDepartment(CustomerDepartmentData department)
        {
            await _context.tb_CustomerDepartment.AddAsync(department);
            await _context.SaveChangesAsync();
        }
      
    }
}
