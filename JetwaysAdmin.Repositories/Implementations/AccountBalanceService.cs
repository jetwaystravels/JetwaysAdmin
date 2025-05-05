using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Repositories.Implementations
{
   

    public class AccountBalanceService : ICustomerAccountBalance<CustomerAccountBalance>
    {
        private readonly AppDbContext _accountbalace;

        public AccountBalanceService(AppDbContext accountbalace) {

            _accountbalace= accountbalace;  
        }


        public async Task AccountBalance(CustomerAccountBalance customerAccountBalance)
        {
            await _accountbalace.tb_CustomerAccountBalance.AddAsync(customerAccountBalance);    
            await _accountbalace.SaveChangesAsync();
        }

       
    }
}
