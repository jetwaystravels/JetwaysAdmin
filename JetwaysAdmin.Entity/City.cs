﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Entity
{
	public class City
	{
		[Key]
		public int CityID { get; set; }
		public int StateID { get; set; }
		public string CityName { get; set; }
	}
}
