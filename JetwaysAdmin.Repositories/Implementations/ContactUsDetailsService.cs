using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Repositories.Implementations
{
    public class ContactUsDetailsService : IContactUsDetails<ContactUsDetails>
    {
        private readonly AppDbContext _appContext;

        public ContactUsDetailsService(AppDbContext appContext)
        {
            _appContext = appContext;
        }
        public async Task AddContactUs(ContactUsDetails contactus)
        {
            await _appContext.tb_ContactUsDetails.AddAsync(contactus);
            await _appContext.SaveChangesAsync();
        }
    }
}
