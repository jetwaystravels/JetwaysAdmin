using JetwaysAdmin.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Repositories.Interface
{
    public interface ISuppliersCredential<T> where T : class
    {
        Task AddSupplierCredential(SuppliersCredential supplierscredential);
        Task<IEnumerable<SuppliersCredential>> GetSupplierCredential(string flightclass);
        Task<IEnumerable<SuppliersCredential>> AdminGetSupplierCredential();
        Task<SuppliersCredential> GetSupplierCredentialById(int Id);
        Task UpdateSupplierCredentialById(SuppliersCredential supplierscredential);
    }
}
