using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;
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

        public async Task<IEnumerable<SupplierDto>> GetSuppliersByLegalEntityAsync(string legalEntityCode)
        {
            var result = await _appContext.SupplierDtos
    .FromSqlInterpolated($"EXEC sp_GetSuppliersByLegalEntity {legalEntityCode}")
    .ToListAsync();

            return result.Select(r => new SupplierDto
            {
                SupplierID = r.SupplierID,
                SupplierName = r.SupplierName,
                SupplierCode = r.SupplierCode,
                SupplierType = r.SupplierType,
                CarrierType = r.CarrierType,
                AddressLine1 = r.AddressLine1,
                AddressLine2 = r.AddressLine2,
                Country = r.Country,
                State = r.State,
                City = r.City,
                PinCode = r.PinCode,
                SupplierEmails = r.SupplierEmails,
                Logo = r.Logo,
                IsActive = r.IsActive
            });

        }

        public async Task AddOrUpdateLegalEntitySupplierStatusAsync(string legalEntityCode, int supplierId, bool isActive)
        {
            var parameters = new[]
            {
            new SqlParameter("@legalEntitycode", legalEntityCode),
            new SqlParameter("@SupplierID", supplierId),
            new SqlParameter("@IsActive", isActive)
        };

            await _appContext.Database.ExecuteSqlRawAsync(
                "EXEC sp_AddOrUpdateLegalEntitySupplierStatus @legalEntitycode, @SupplierID, @IsActive",
                parameters
            );
        }


        public async Task UpdateSupplierById(AddSupplier supplier)
        {
            _appContext.tb_SuppliersDetail.Update(supplier);
            await _appContext.SaveChangesAsync();
        }
    }
}
