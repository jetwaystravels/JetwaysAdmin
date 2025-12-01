using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Entity
{
    public class MenuGroupDto
    {
        public int MenuId { get; set; }          // "menuId"
        public string Title { get; set; }        // "title"
        public bool IsActive { get; set; }       // "isActive"
        public List<SubMenuDto> SubMenus { get; set; } = new();  // "subMenus"
    }

    public class SubMenuDto
    {
        public int Id { get; set; }              // "id"
        public string Name { get; set; }         // "name"
        public string Url { get; set; }          // "url"
        public string Action { get; set; }       // "action"
        public bool IsActive { get; set; }       // "isActive"
    }
}
