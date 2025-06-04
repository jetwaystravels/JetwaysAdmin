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
    public class CustomersEmployeeService : ICustomersEmployee<CustomersEmployee>
    {
        private readonly AppDbContext _context;

        public CustomersEmployeeService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CustomersEmployee>> GetAllCustomerEmployee()
        {
            return await _context.tb_CustomersEmployee.ToListAsync();
        }

        public async Task<CustomersEmployee> GetUsersById(int id)
        {
            return await _context.tb_CustomersEmployee.FirstOrDefaultAsync(e => e.UserID == id);
        }


        public async Task Addusers(CustomersEmployee UserAdd)
        {
            await _context.tb_CustomersEmployee.AddAsync(UserAdd);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUsersById(CustomersEmployee Users)
        {
            _context.tb_CustomersEmployee.Update(Users);
            await _context.SaveChangesAsync();
        }

      

        
    }
}
