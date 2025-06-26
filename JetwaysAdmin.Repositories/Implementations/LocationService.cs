using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Repositories.Implementations
{
	public class LocationService : ILocation<AddressCountryState>
	{
		private readonly AppDbContext _context;

		public LocationService(AppDbContext context)
		{
			_context = context;
		}
		public AddressCountryState GetAll()
		{
			return new AddressCountryState
			{
				Countries = _context.tb_Country.ToList(),
				States = _context.tb_State.ToList(),
				Cities = _context.tb_City.ToList()
			};
		}
		public AddressCountryState GetByCountryId(int countryId)
		{
			var states = _context.tb_State.Where(s => s.CountryID == countryId).ToList();
			return new AddressCountryState
			{
				States = states
			};
		}
		public AddressCountryState GetByStateId(int stateId)
		{
			var cities = _context.tb_City.Where(c => c.StateID == stateId).ToList();
			return new AddressCountryState
			{
				Cities = cities
			};
		}
	}

}
