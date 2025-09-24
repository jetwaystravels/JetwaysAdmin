using JetwaysAdmin.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Repositories.Interface
{
    public interface ICustomersEmployee<T> where T : class
    {
        //Task<IEnumerable<CustomersEmployee>> GetAllCustomerEmployee();
        Task<IEnumerable<CustomersEmployee>> GetCustomerEmployeeByLegalEntity(string legalEntityCode);

        Task<CustomersEmployee> GetUsersById(int id);
        Task Addusers(CustomersEmployee UserAdd);
        Task UpdateUsersById(CustomersEmployee Users);
        // ✅ new: update only the password (already hashed value)
        Task<bool> UpdatePasswordAsync(int userId, string passwordHash);

    }
}
