﻿using JetwaysAdmin.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Repositories.Interface
{
    public interface IAddNewSupplier<T> where T : class
    {
      Task AddNewSupplier(AddSupplier addSupplier);
        Task<IEnumerable<AddSupplier>> GetSupplier();
        Task<IEnumerable<SupplierDto>>GetSuppliersByLegalEntityAsync(string legalEntityCode);
        Task AddOrUpdateLegalEntitySupplierStatusAsync(string legalEntityCode, int supplierId, bool isActive);
        Task<AddSupplier> GetSupplierById(int id);

        Task UpdateSupplierById(AddSupplier supplier);

    }
}
