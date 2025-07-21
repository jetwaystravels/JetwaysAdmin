using JetwaysAdmin.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Repositories.Interface
{
    public interface IDealCode<T> where T : class
    {
        Task AddDealCode(DealCode dealCode);
        Task<IEnumerable<DealCode>> GetDealCode();

        Task<IEnumerable<DealCode>> GetDealCodeSupplierId(int Supplierid);
        Task<DealCode> GetDealCodeById(int DealCodeId);
        Task UpdateDealCodeById(DealCode dealCode);

      
    }
}
