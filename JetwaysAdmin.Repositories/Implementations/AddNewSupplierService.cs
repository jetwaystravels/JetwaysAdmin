using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Repositories.Implementations
{
    public class AddNewSupplierService : IAddNewSupplier<AddSupplier>
    {
        private readonly AppDbContext _appContext;

        public AddNewSupplierService(AppDbContext appContext)
        {
            _appContext = appContext;
        }

        public async Task AddNewSupplier(AddSupplier addSupplier)
        {
            await _appContext.tb_SuppliersDetail.AddAsync(addSupplier); 
            await _appContext.SaveChangesAsync();   
        }

        public async Task<IEnumerable<AddSupplier>> GetSupplier()
        {
            return await _appContext.tb_SuppliersDetail.ToListAsync();
        }

        public async Task<AddSupplier> GetSupplierById(int id)
        {
            return await _appContext.tb_SuppliersDetail.FirstOrDefaultAsync(e => e.SupplierId == id);
        }

       
        public async Task UpdateSupplierById(AddSupplier supplier)
        {
            _appContext.tb_SuppliersDetail.Update(supplier);
            await _appContext.SaveChangesAsync();
        }
    }
}
