using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Entity
{
    public class MenuItemApiDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Action { get; set; }
        public string Url { get; set; }
        public int? ParentId { get; set; }

        // MUST be bool because API sends true/false
        public bool IsActive { get; set; }
    }
}
