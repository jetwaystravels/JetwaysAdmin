using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using JetwaysAdmin.Repositories.Migrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Repositories.Implementations
{
    public class SuppliersCredentialService: ISuppliersCredential<SuppliersCredential>
    {
        private readonly AppDbContext _context;
        public SuppliersCredentialService(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddSupplierCredential(SuppliersCredential supplierscredential)
        {
            await _context.tb_SuppliersCredential.AddAsync(supplierscredential);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<SuppliersCredential>> GetSupplierCredential( string flightclass)
        {
            //charan start
            List<SuppliersCredential> suppliersCredentials = new List<SuppliersCredential>();
            List<SuppliersCredential> suppliersCredentials1 = new List<SuppliersCredential>();
            suppliersCredentials1 = await _context.tb_SuppliersCredential
                         .AsNoTracking()
             .Where(sc => sc.Status == 1 && sc.AssociatedFareTypes == flightclass && _context.tb_SuppliersDetail.Any(sd =>
               sd.SupplierId == sc.SupplierId && sd.AppStatus == true))
             .ToListAsync();

           // var dc = _context.tb_SuppliersDealCode.Where(d => d.AssociatedFareTypes == "Corporate" && d.SupplierId == 1).ToList();

            foreach (var supplier in suppliersCredentials1)
            {
                if (supplier.Status == 1)
                {
                    string dcodename = string.Empty;
                    if (_context.tb_SuppliersDealCode.Where(d => d.AssociatedFareTypes == flightclass && d.SupplierId == supplier.SupplierId) != null)
                    {
                        // Get the deal code name for the supplier
                         dcodename = _context.tb_SuppliersDealCode.Where(d => d.AssociatedFareTypes == flightclass && d.SupplierId == supplier.SupplierId).FirstOrDefault().DealCodeName;
                       // supplier.DealCodeName = dcodename;
                    }

                    supplier.DealCodeName = dcodename;
                    suppliersCredentials.Add(supplier);
                }
            }
            //charan end
            return suppliersCredentials;

            //return await _context.tb_SuppliersCredential
            //             .Where(sc => sc.Status == 1)
            //             .AsNoTracking()
            //             .ToListAsync();
        }
        public async Task<SuppliersCredential> GetSupplierCredentialById(int Id)
        {
            return await _context.tb_SuppliersCredential.FirstOrDefaultAsync(e => e.Id == Id);
        }
        public async Task UpdateSupplierCredentialById(SuppliersCredential supplierscredential)
        {
            _context.tb_SuppliersCredential.Update(supplierscredential);
            await _context.SaveChangesAsync();
        }
    }
}
