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

        public AddressCountryState GetAllStates()
        {
            return new AddressCountryState
            {
                States = _context.tb_State.ToList()
            };
        }
        public AddressCountryState GetAllCity()
        {
            return new AddressCountryState
            {
                Cities = _context.tb_City.ToList()
            };
        }
        
        //public async Task<AddressCountryState?> GetStatebylegalentityAsync(string legalentitycode)
        //{
        //    return await _context.Admin_tb_LegalEntity
        //        .AsNoTracking()
        //        .Where(x => x.LegalEntityCode == legalentitycode)
        //        .Select(x => new AddressCountryState
        //        {
        //            // Map your columns appropriately
        //            // StateCode = x.StateCode,   // if you have StateCode column
        //            Stateid = x.State        // if you want the name/value
        //        })
        //        .FirstOrDefaultAsync();
        //}

        public async Task<AddressCountryState?> GetStatebylegalentityAsync(string legalentitycode)
        {
            return await (
                from le in _context.Admin_tb_LegalEntity.AsNoTracking()
                join s in _context.tb_State.AsNoTracking()
                    on le.State equals s.StateID.ToString() // Ensure both sides of the join are of the same type
                where le.LegalEntityCode == legalentitycode
                select new AddressCountryState
                {
                    Stateid = le.State,                 // from LegalEntity
                    StateName = s.StateName             // from tb_State
                }
            ).FirstOrDefaultAsync();
        }

    }

}
