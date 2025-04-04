using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetwaysAdmin.Entity;

namespace JetwaysAdmin.Repositories.Interface
{
    public interface ICustomerDetailsByEmail<T> where T : class
    {
        Task<IEnumerable<CustomerDetails>> GetCustomerDetailsByEmailAsync(string email);

    }
}
