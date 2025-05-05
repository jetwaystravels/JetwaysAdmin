using JetwaysAdmin.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Repositories.Interface
{
    public interface IAccountDetails<T> where T : class
    {
        Task AddAccountDetails(AccountDetails accountdetails);
    }
}
