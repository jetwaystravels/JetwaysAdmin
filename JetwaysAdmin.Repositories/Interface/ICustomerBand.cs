using JetwaysAdmin.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Repositories.Interface
{
    public interface ICustomerBand<T> where T : class
    {
        Task<IEnumerable<CustomerBand>> GetAllBand();
        Task AddCustomerBand(CustomerBand customerband);
        Task<CustomerBand> GetCustomerBandById(int BandID);
        Task UpdateBandData(CustomerBand customerband);
    }
}
