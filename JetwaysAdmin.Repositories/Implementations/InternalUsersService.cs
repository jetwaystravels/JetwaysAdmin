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
    public class InternalUsersService: IInternalUsers<InternalUsers>
    {
        private readonly AppDbContext _internaluser;
        public InternalUsersService(AppDbContext internaluser) 
        {
            _internaluser = internaluser;
        }

        public async Task AddInternalusers(InternalUsers internalUsers)
        {
            await _internaluser.tb_InternalUsers.AddAsync(internalUsers);
            await _internaluser.SaveChangesAsync();
        }

        public async Task<IEnumerable<InternalUsers>> GetInternalUsers()
        {
            return await _internaluser.tb_InternalUsers.ToListAsync();
        }


        public async Task<InternalUsers> GetInternalUsersById(int id)
        {
            return await _internaluser.tb_InternalUsers.FirstOrDefaultAsync(e => e.UserID == id);
        }

        public async Task UpdateInternalUsersById(InternalUsers internalUsers)
        {
            _internaluser.tb_InternalUsers.Update(internalUsers);
            await _internaluser.SaveChangesAsync();
        }

    }
}
