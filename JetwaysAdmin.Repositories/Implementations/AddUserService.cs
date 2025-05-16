using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Repositories.Implementations
{
    public class AddUserService : IAddUser<AddUser>
    {

        private readonly AppDbContext _adduser;

        public AddUserService(AppDbContext adduser) {
            _adduser=adduser;   
        }
        public async Task AddUser(AddUser adduser)
        {
          await _adduser.tb_AddNewUser.AddAsync(adduser);
          await _adduser.SaveChangesAsync();
        }

        public  async Task<IEnumerable<AddUser>> GetManageUser()
        {
            return await _adduser.tb_AddNewUser.ToListAsync();
        }

       
    }
}
