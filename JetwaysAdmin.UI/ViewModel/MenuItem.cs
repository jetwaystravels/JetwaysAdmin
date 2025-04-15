using JetwaysAdmin.Entity;
using System.ComponentModel.DataAnnotations;

namespace JetwaysAdmin.UI.ViewModel
{
    public class MenuItemdata
    {
       
            public int Id { get; set; }
            public string Name { get; set; }

            public string Action { get; set; }

            public string Url { get; set; }
            public int? ParentId { get; set; }
            public bool IsActive { get; set; }
  


    }

    public class MenuHeaddata
    {
        [Key]
        public int MenuId { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }

        public ICollection<MenuItemdata> SubMenus { get; set; }  // ✅ Navigation property
    }
}
