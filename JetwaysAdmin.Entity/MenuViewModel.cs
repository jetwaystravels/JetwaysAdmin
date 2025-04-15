using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Entity
{
    public class MenuViewModel
    {

        public int MenuId { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public List<SubMenuViewModel> SubMenus { get; set; }
    }

    public class SubMenuViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Action { get; set; }
        public bool IsActive { get; set; }
    }
}
