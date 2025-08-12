using JetwaysAdmin.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Repositories.Interface
{
    public interface ICustomerDepartment<T> where T : class
    {
        Task AddCustomerDepartment(CustomerDepartmentData department);
        Task<IEnumerable<CustomerDepartmentData>> GetAllCustomerDepartment(string legalEntityCode);
    }
}
