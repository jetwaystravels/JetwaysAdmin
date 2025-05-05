using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Repositories.Implementations
{
    public class DashboardService : IDashboard<DashboardSummary>
    {
        private readonly AppDbContext _appDbContext;
        public DashboardService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<DashboardSummary> GetDashboardSummaryAsync()
        {
            return new DashboardSummary
            {
                CustomerCount = await _appDbContext.Admin_tb_LegalEntity.CountAsync(),
                //SupplierCount = await _appDbContext.Suppliers.CountAsync(),
                //IATAGroupsCount = await _appDbContext.IATAGroups.CountAsync(),
                //IATACommissionsCount = await _appDbContext.IATACommissions.CountAsync(),
                //MiniFareRulesCount = await _appDbContext.MiniFareRules.CountAsync(),
                //ReportingParametersCount = await _appDbContext.ReportingParameters.CountAsync(),
                //CabsCount = await _appDbContext.Cabs.CountAsync(),
                //BannersCount = await _appDbContext.Banners.CountAsync()
            };
        }
    }
}
