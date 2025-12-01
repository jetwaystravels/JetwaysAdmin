using Microsoft.AspNetCore.Mvc.Rendering;

namespace JetwaysAdmin.UI.Models
{
    public class MenuRightRowVM
    {
        public int MenuId { get; set; }
        public string HeaderTitle { get; set; }
        public string ItemName { get; set; }
        public bool CanView { get; set; }
        public bool CanAdd { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
    }

    public class UserMenuRightsVM
    {
        public int UserId { get; set; }
        public string UserName { get; set; }

        public List<MenuRightRowVM> Rights { get; set; } = new();

        // ✅ Now SelectListItem works, because this is in the UI project
        public List<SelectListItem> UserOptions { get; set; } = new();
    }
}
