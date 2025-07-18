using JetwaysAdmin.Entity;

namespace JetwaysAdmin.UI.ViewModel
{
    public class ManageStaff
    {
        public List<InternalUsers> InternalUsers { get; set; }
        public List<int> AssignedBookingConsultants { get; set; } // add this
    }
}
