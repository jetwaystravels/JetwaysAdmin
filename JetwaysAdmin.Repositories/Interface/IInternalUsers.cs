using JetwaysAdmin.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Repositories.Interface
{
    public interface IInternalUsers<T> where T : class
    {
        Task AddInternalusers(InternalUsers internalUsers);
        Task<IEnumerable<InternalUsers>> GetInternalUsers();
        Task<InternalUsers> GetInternalUsersById(int id);
       
        Task UpdateInternalUsersById(InternalUsers internalUsers);

        Task<InternalUsers> LoginAsync(string businessEmail, string password);
    }
}
