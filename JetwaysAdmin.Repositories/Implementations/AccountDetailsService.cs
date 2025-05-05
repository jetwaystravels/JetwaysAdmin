using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using JetwaysAdmin.Repositories.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Repositories.Implementations
{
    public class AccountDetailsService : IAccountDetails<AccountDetails>
    {
        private readonly AppDbContext _context;

        public AccountDetailsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAccountDetails(AccountDetails accountdetails)
        {
            await _context.tb_CustomersAccountDetail.AddAsync(accountdetails);
            await _context.SaveChangesAsync();
        }
    }
}
