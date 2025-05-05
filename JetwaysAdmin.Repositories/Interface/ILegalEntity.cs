using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetwaysAdmin.Entity;

namespace JetwaysAdmin.Repositories.Interface
{
    public interface ILegalEntity<T> where T : class
    {
        Task<IEnumerable<LegalEntity>> GetAllLegalEntity();
        Task<LegalEntity> GetLegalEntityById(int id);
        Task AddLegalEntity(LegalEntity legalEntity);
        Task UpdateLegalEntity(LegalEntity legalEntity);
        Task DeleteLegalEntity(int id);


    }
}
