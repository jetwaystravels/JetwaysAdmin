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
        public List<MenuItemdata> SubMenus { get; set; }  
        public List<IATAGroupView> IATAGruopName { get; set; }

       

    }

 




    public class IATAGroupView
    {
        [Key]
        public int GroupID { get; set; }
        public string IATACode { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyByDate { get; set; }
        public bool IsActive { get; set; }
    }


}
