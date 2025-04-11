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
    public class MenuService : IMenu<MenuItem>
    {

        private readonly AppDbContext _context;

        public MenuService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MenuItem>> GetAllAsync()
        {
            return await _context.tb_Menu.ToListAsync();
        }
        public async Task<IEnumerable<MenuItem>> GetActiveMenusAsync()
        {
            return await _context.tb_Menu.Where(m => m.IsActive==1).ToListAsync();
        }

        public async Task AddAsync(MenuItem menuItem)
        {
            await _context.tb_Menu.AddAsync(menuItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var menuItem = await _context.tb_Menu.FindAsync(id);
            if (menuItem != null)
            {
                _context.tb_Menu.Remove(menuItem);
                await _context.SaveChangesAsync();
            }
        }
 
       

        public async Task<MenuItem> GetByIdAsync(int id)
        {
            return await _context.tb_Menu.FindAsync(id);
        }

        public async Task UpdateAsync(MenuItem menuItem)
        {
            _context.tb_Menu.Update(menuItem);
            await _context.SaveChangesAsync();
        }
    }
}
