using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Entity
{
	public class AddressCountryState
	{
		public List<Country>? Countries { get; set; }
		public List<State>? States { get; set; }
		public List<City>? Cities { get; set; }

        public string? Stateid { get; set; }
        public string? StateName { get; set; }
    }
}
