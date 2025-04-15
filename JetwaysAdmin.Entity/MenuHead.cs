using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Entity
{
   
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Action { get; set; }
        public bool IsActive { get; set; }

        public int ParentId { get; set; }  // ✅ FK to MenuHead.MenuId

        public MenuHead MenuHead { get; set; }  // ✅ Navigation property
    }

    public class MenuHead
    {
        [Key]
        public int MenuId { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Menu> SubMenus { get; set; }  // ✅ Navigation property
    }
}
