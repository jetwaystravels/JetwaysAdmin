﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Entity
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Action { get; set; }

        public string Url { get; set; }
        public int? ParentId { get; set; }
        public int? IsActive { get; set; }

    }


}
