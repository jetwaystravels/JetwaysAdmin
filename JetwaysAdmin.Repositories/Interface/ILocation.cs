using JetwaysAdmin.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Repositories.Interface
{
	public interface ILocation<T>
	{
		T GetAll();
        T GetAllStates();
        T GetByCountryId(int countryId);
		T GetByStateId(int stateId);
	}
}
