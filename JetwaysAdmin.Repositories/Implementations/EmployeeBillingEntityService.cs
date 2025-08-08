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
    public class EmployeeBillingEntityService : IEmployeeBillingEntity<EmployeeBillingEntity>
    {
        private readonly AppDbContext _context;

        public EmployeeBillingEntityService(AppDbContext context)
        {
            _context = context;
        }

        //billing entity
        public async Task AddEmplBillingEntity(EmployeeBillingEntity emplBillingEntity)
        {
            await _context.tb_EmployeeBillingEntity.AddAsync(emplBillingEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<EmployeeBillingEntity> GetEmplBillingEntityById(int id)
        {
            return await _context.tb_EmployeeBillingEntity.FirstOrDefaultAsync(e => e.UserID == id);
        }

        public async Task<CustomerDealCodes> GetsupplierdealcodeById(string legalcode)
        {
            return await _context.CustomerDealCodes.FirstOrDefaultAsync(e => e.LegalEntityCode == legalcode  && e.Status == 1);
        }

    }
}
