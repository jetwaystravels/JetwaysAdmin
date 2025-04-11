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
    public class CustomerService : ICustomer<Customer>
    {
        private readonly AppDbContext _context;

        public CustomerService(AppDbContext context )
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await _context.Admin_tb_Customers.ToListAsync();
        }

        public async Task<int> GetCustomerCount()
        {
            return await _context.Admin_tb_Customers.CountAsync();
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            return await _context.Admin_tb_Customers.FindAsync(id);
        }

        public async Task AddCustomer(Customer customer)
        {
            await _context.Admin_tb_Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCustomer(Customer customer)
        {
            _context.Admin_tb_Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomer(int id)
        {
            var customer = await _context.Admin_tb_Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Admin_tb_Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }

       
    }
}
