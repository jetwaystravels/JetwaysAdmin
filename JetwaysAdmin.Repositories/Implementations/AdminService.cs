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
 
    public class AdminService : IAdmin<Admin>
    {
        private readonly AppDbContext _context;
        public AdminService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Admin>> GetAllAsync()
        {
           // return await _context.Set<Admin>().ToListAsync();

             return await _context.tb_admin.ToListAsync();
        }

        public async Task AddAsync(Admin admin)
        {
            //await _context.Set<Admin>().AddAsync(admin);
            await _context.tb_admin.AddAsync(admin);
            await _context.SaveChangesAsync();
        }

        public async Task<Admin> Login(string username, string password)
        {
            return _context.tb_admin.FirstOrDefault(a => a.admin_email == username && a.admin_password == password );
            //return await _context.tb_admin.FindAsync(username, password);
        }


        public async Task<Admin> GetByIdAsync(int id)
        {
            // return await _context.Set<Admin>().FindAsync(id);
            return await _context.tb_admin.FindAsync(id);
            
        }

        public async Task UpdateAsync(Admin admin)
        {
           // _context.Set<Admin>().Update(admin);
            _context.tb_admin.Update(admin);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var admin = await GetByIdAsync(id);
            if (admin != null)
            {
              //  _context.Set<Admin>().Remove(admin);
                _context.tb_admin.Remove(admin);
                await _context.SaveChangesAsync();
            }
        }
    }
}
