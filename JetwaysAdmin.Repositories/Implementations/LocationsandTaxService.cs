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
    public class LocationsandTaxService: ILocationsandTax<LocationsandTax>
    {
        private readonly AppDbContext _addtax;

        public LocationsandTaxService(AppDbContext addtax)
        {
            _addtax = addtax;
        }

        public async Task AddLocationTax(LocationsandTax locationTax)
        {
            await _addtax.tb_CustomerLocationTaxDetails.AddAsync(locationTax);
            await _addtax.SaveChangesAsync();
        }

        public async Task<IEnumerable<LocationsandTax>> GetLocationsandTaxByLegalEntity(string legalEntityCode)
        {
            return await _addtax.tb_CustomerLocationTaxDetails
                .Where(emp => emp.LegalEntityCode == legalEntityCode)
                .ToListAsync();
        }
    }
}
