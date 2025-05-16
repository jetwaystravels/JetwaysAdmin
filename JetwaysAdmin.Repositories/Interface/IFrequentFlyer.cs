using JetwaysAdmin.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Repositories.Interface
{
    public interface IFrequentFlyer<T> where T : class
    {
        Task AddFrequentFlyer(EmployeeFrequentFlyer frequentFlyer);
        Task<IEnumerable<EmployeeFrequentFlyer>> GetAllFrequentFlyer();
        Task<EmployeeFrequentFlyer> GetFrequentFlyerById(int id);
        Task UpdateFrequentFlyer(EmployeeFrequentFlyer frequentFlyer);

    }
}
