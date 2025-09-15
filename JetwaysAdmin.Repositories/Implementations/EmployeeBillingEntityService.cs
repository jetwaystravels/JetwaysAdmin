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
    public class EmployeeBillingEntityService : IEmployeeBillingEntity<EmployeeBillingEntity>
    {
        private readonly AppDbContext _context;

        public EmployeeBillingEntityService(AppDbContext context)
        {
            _context = context;
        }

        //billing entity
        //public async Task AddEmplBillingEntity(EmployeeBillingEntity emplBillingEntity)
        //{
        //    await _context.tb_EmployeeBillingEntity.AddAsync(emplBillingEntity);
        //    await _context.SaveChangesAsync();
        //}

        public async Task AddEmplBillingEntity(EmployeeBillingEntity emplBillingEntity)
        {
            var sql = "EXEC sp_AddEmployeeBillingEntity " +
                      "@UserID, @LegalEntityCode, @BillingEntityCode, @Designation, @Department, " +
                      "@Band, @WorkLocation, @ReportingManager, @EmploymentType, @CostCenter, " +
                      "@UserRole, @DateOfJoining, @DateOfBirth, @SystemIntegrationRef, " +
                      "@CreatedBy, @CreatedDate, @ModifyBy, @ModifyDate, @AppStatus";

            await _context.Database.ExecuteSqlRawAsync(sql,
                new SqlParameter("@UserID", emplBillingEntity.UserID),
                new SqlParameter("@LegalEntityCode", (object?)emplBillingEntity.LegalEntityCode ?? DBNull.Value),
                new SqlParameter("@BillingEntityCode", (object?)emplBillingEntity.BillingEntityCode ?? DBNull.Value),
                new SqlParameter("@Designation", (object?)emplBillingEntity.Designation ?? DBNull.Value),
                new SqlParameter("@Department", (object?)emplBillingEntity.Department ?? DBNull.Value),
                new SqlParameter("@Band", (object?)emplBillingEntity.Band ?? DBNull.Value),
                new SqlParameter("@WorkLocation", (object?)emplBillingEntity.WorkLocation ?? DBNull.Value),
                new SqlParameter("@ReportingManager", (object?)emplBillingEntity.ReportingManager ?? DBNull.Value),
                new SqlParameter("@EmploymentType", (object?)emplBillingEntity.EmploymentType ?? DBNull.Value),
                new SqlParameter("@CostCenter", (object?)emplBillingEntity.CostCenter ?? DBNull.Value),
                new SqlParameter("@UserRole", (object?)emplBillingEntity.UserRole ?? DBNull.Value),
                new SqlParameter("@DateOfJoining", (object?)emplBillingEntity.DateOfJoining ?? DBNull.Value),
                new SqlParameter("@DateOfBirth", (object?)emplBillingEntity.DateOfBirth ?? DBNull.Value),
                new SqlParameter("@SystemIntegrationRef", (object?)emplBillingEntity.SystemIntegrationRef ?? DBNull.Value),
                new SqlParameter("@CreatedBy", emplBillingEntity.CreatedBy),
                new SqlParameter("@CreatedDate", (object?)emplBillingEntity.CreatedDate ?? DBNull.Value),
                new SqlParameter("@ModifyBy", emplBillingEntity.ModifyBy),
                new SqlParameter("@ModifyDate", (object?)emplBillingEntity.ModifyDate ?? DBNull.Value),
                new SqlParameter("@AppStatus", emplBillingEntity.AppStatus)
            );
        }


        public async Task<EmployeeBillingEntity> GetEmplBillingEntityById(int id)
        {
            return await _context.tb_EmployeeBillingEntity.FirstOrDefaultAsync(e => e.UserID == id);
        }

        public async Task<CustomerDealCodes> GetsupplierdealcodeById(string legalcode)
        {
            return await _context.CustomerDealCodes.FirstOrDefaultAsync(e => e.LegalEntityCode == legalcode  && e.Status == 1);
        }

    }
}
