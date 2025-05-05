using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Repositories.Interface
{
    public interface ICustomerAccountBalance<T> where T : class
    {
        Task AccountBalance(CustomerAccountBalance accountBalance);
    }
}
