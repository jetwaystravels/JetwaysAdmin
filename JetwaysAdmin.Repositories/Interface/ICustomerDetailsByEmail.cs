using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetwaysAdmin.Entity;

namespace JetwaysAdmin.Repositories.Interface
{
    public interface ICustomerDetailsByEmail<CustomerDetails, CustomerDealCodes>
    where CustomerDetails : class
    where CustomerDealCodes : class
    {
        Task<IEnumerable<CustomerDetails>> GetCustomerDetailsByEmailAsync(string email);
        Task<IEnumerable<CustomerDealCodes>> GetCustomerdealCodeAsync(string legalEntityCode, string Iata);
    }
}
