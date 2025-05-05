using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace JetwaysAdmin.Repositories.Interface
{
    public interface IHierarchyLegalEntity<T> where T : class
    {
        Task<IEnumerable<HierarchyLegalEntity>> GetHierarchicallegalentityAsync(string legalEntityCode);
    }
}
