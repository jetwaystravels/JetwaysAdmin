using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Entity
{
	public class State
	{
		[Key]
		public int StateID { get; set; }
		public int CountryID { get; set; }
		public string StateName { get; set; }
	}
}
