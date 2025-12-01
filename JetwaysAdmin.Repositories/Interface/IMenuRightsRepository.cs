using JetwaysAdmin.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Repositories.Interface
{
    public interface IMenuRightsRepository
    {
        Task SaveUserRightsAsync(UserMenuRightsDto model);
        Task<List<MenuRightDto>> GetUserRightsAsync(int userId);
    }
}