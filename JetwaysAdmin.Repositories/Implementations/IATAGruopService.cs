using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Repositories.Implementations
{
    public class IATAGruopService : IIATAGroup<IATAGroup>
    {
        private readonly AppDbContext _context;

        public IATAGruopService(AppDbContext context) {
            _context = context;
        }

        public async Task<List<IATAGroup>> GetIATAGruop()
        {
            //  return _context.tb_IATAGroup.
            return await _context.tb_IATAGroup
                .Where(g => g.IsActive)
                .Select(g => new IATAGroup
                {
                    GroupID = g.GroupID,
                    GroupName = g.GroupName
                })
                .ToListAsync();
        }

    }
}
