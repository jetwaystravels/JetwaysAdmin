using JetwaysAdmin.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Repositories.Interface
{
    public interface IAddUser<T> where T : class
    {
        Task AddUser(AddUser addUser);
        Task<IEnumerable<AddUser>> GetManageUser();

    }
}
