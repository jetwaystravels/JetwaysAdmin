using JetwaysAdmin.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Repositories.Interface
{
    public interface ICustomerDesignation<T> where T : class
    {
        Task AddCustomerDesignation(CustomerDesignation designation);
        Task<IEnumerable<CustomerDesignation>> GetAllCustomerDesignation();
        Task<CustomerDesignation> GetCustomerDesignationById(int DesignationID);
        Task UpdateDesignationData(CustomerDesignation Designation);
    }
}
