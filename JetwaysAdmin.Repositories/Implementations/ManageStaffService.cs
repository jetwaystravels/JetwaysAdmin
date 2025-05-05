using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Repositories.Implementations
{
    public class ManageStaffService : IManageStaff<CustomerManageStaff>
    {
        private readonly AppDbContext _customermanagestaff;

        public ManageStaffService(AppDbContext customermanagestaff)
        {
            _customermanagestaff = customermanagestaff;
        }

       public async Task ManageStaff(CustomerManageStaff customermanagestaff)
        {
            await _customermanagestaff.tb_CustomerManageStaff.AddAsync(customermanagestaff);
            await _customermanagestaff.SaveChangesAsync();  
        }
    }
}
