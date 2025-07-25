using JetwaysAdmin.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Repositories.Interface
{
    public interface ILocationsandTax<T> where T : class
    {
        Task AddLocationTax(LocationsandTax locationTax);
        Task<IEnumerable<LocationsandTax>> GetLocationsandTaxByLegalEntity(string legalEntityCode);
    }
}
