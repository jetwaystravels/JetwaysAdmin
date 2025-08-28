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
        public List<AddUser> usermanage { get; set; }
        public List<AddSupplier> getsupplier { get; set; }
        public List<CustomersEmployee> customersemployee { get; set; }
        public List<EmployeeFrequentFlyer> flyerList { get; set; }
        public List<FrequentFlyerDisplay> FlyerDisplayList { get; set; }
        public List<InternalUsers> InternalUsers { get; set; }
        public List<LegalEntity> LegalEntitydata { get; set; }
        public List<SuppliersCredential> supplierscredential { get; set; }
        public List<DealCode> DealCodeView { get; set; }
        public BookingConsultantDto BookingConsultants { get; set; }
        public List<LocationsandTax> LocationandTax { get; set; }
        public List<State> Statedata { get; set;}
        public List<CustomerDepartmentData> Customerdepartment { get; set;}
        public List<CustomerDesignation> Customerdesignation { get; set;}
        public List<LegalEntitySupplierDto> LegalEntitySupplierstatus{ get; set;}

    }

    public class UserProfileMenuHeaddata
    {
        public MenuHeaddata LegalData { get; set; }
        public List<LegalEntity> LegalEntitydata { get; set; }
        public List<CustomersEmployee> CustomerDetail { get; set; }
    }

    public class FrequentFlyerDisplay
    {
        public int FrequentFlyerID { get; set; }
        public string EmployeeID { get; set; }

        public int UserID { get; set; }
        public string EmployeeName { get; set; }
        public string SupplierName { get; set; }
        public string SupplierCode { get; set; }
        public string FrequentFlyerNumber { get; set; }
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
