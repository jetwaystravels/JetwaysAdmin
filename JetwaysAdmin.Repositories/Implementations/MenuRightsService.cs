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
    public class MenuRightsService : IMenuRightsRepository
    {
        private readonly AppDbContext _appContext;

        public MenuRightsService(AppDbContext appContext)
        {
            _appContext = appContext;
        }

        public async Task SaveUserRightsAsync(UserMenuRightsDto model)
        {
            // Load ALL existing rights for that user
            var existing = await _appContext.MenuRights
                .Where(r => r.UserId == model.UserId)
                .ToListAsync();

            foreach (var right in model.Rights)
            {
                var row = existing.FirstOrDefault(r => r.MenuId == right.MenuId);

                bool anyTrue = right.CanView || right.CanAdd || right.CanEdit || right.CanDelete;

                // CASE 1: RECORD EXISTS → UPDATE OR DELETE
                if (row != null)
                {
                    if (!anyTrue)
                    {
                        // delete if no permissions
                        _appContext.MenuRights.Remove(row);
                    }
                    else
                    {
                        // update existing record
                        row.CanView = right.CanView;
                        row.CanAdd = right.CanAdd;
                        row.CanEdit = right.CanEdit;
                        row.CanDelete = right.CanDelete;
                    }
                }
                else
                {
                    // CASE 2: RECORD DOES NOT EXIST → INSERT ONLY IF TRUE
                    if (anyTrue)
                    {
                        _appContext.MenuRights.Add(new Tb_MenuRight
                        {
                            UserId = model.UserId,
                            MenuId = right.MenuId,
                            CanView = right.CanView,
                            CanAdd = right.CanAdd,
                            CanEdit = right.CanEdit,
                            CanDelete = right.CanDelete
                        });
                    }
                }
            }

            await _appContext.SaveChangesAsync();
        }
        public async Task<List<MenuRightDto>> GetUserRightsAsync(int userId)
        {
            return await _appContext.MenuRights
                .Where(r => r.UserId == userId)
                .Select(r => new MenuRightDto
                {
                    MenuId = r.MenuId,
                    CanView = r.CanView,
                    CanAdd = r.CanAdd,
                    CanEdit = r.CanEdit,
                    CanDelete = r.CanDelete
                })
                .ToListAsync();
        }

    }
}

