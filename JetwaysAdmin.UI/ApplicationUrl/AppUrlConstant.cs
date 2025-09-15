namespace JetwaysAdmin.UI.ApplicationUrl
{
    public static class AppUrlConstant
    {
        public static string BaseURL = "http://localhost:7260/";
             
        #region login URLs
        public static string login = BaseURL + "api/Admin/LogIn";

        public static string AddAdmin = BaseURL + "api/Admin/AddAdmin";

        public static string getAdminID = BaseURL + "api/Admin/GetAdminID";

        public static string getmenu= BaseURL + "api/Menu/MenuList";
        public static string GetmenuList = BaseURL + "api/Menu/MenuListData";

        public static string getcustomercount = BaseURL + "api/Customer/count";

        public static string AddLegalEntity = BaseURL + "api/LegalEntityAPI/AddLegalEntity";
       
        public static string GetLegalEntity = BaseURL + "api/LegalEntityAPI/GetAllLegalEntity";

        public static string GetLegalEntityID = BaseURL + "api/LegalEntityAPI";

        public static string EditLegalEntityID = BaseURL + "api/LegalEntityAPI";

        public static string GetIATAGroup = BaseURL + "api/IATAGruopAPI/GetIATAGroup";

        public static string AddAccountDetails = BaseURL + "api/AccountDetailsAPI/AddAccountDetails";

        public static string AddNewUser = BaseURL + "api/AddUserAPI/AddUser";
        public static string Manageuser = BaseURL + "api/AddUserAPI/GetManageUser";

        public static string AddAccountBalance = BaseURL + "api/AccountBalanceAPI/AccountBalance";

        public static string ManageStaff = BaseURL + "api/ManageStaffAPI/ManageStaff";
        public static string GetBookingConsultants = BaseURL + "api/ManageStaffAPI/BookingConsultants";


        public static string Dashboard = BaseURL + "api/DashboardAPI/Dashboard";

        public static string LegalHeirachy = BaseURL + "api/Customer/Gethierarchicallegal";

        public static string AddSupplier = BaseURL + "api/SupplierAPI/AddSupplier";
        public static string GetSupplier = BaseURL + "api/SupplierAPI/GetSupplier";
        public static string GetSuppliersLegalEntity = BaseURL + "api/SupplierAPI/legalentitysuppliers";

        public static string GetSupplierID = BaseURL + "api/SupplierAPI";

        public static string EditSupplierID = BaseURL + "api/SupplierAPI";
        public static string Updatelegalentitysupplierstatus = BaseURL + "api/SupplierAPI/Updatelegalentitysuppliers";

        public static string GetCustomerEmployee = BaseURL + "api/CustomersEmployeeAPI/GetCustomerEmployee";

        public static string GetCustomerEmployeeID = BaseURL + "api/CustomersEmployeeAPI";
        public static string AddCustomerEmployee = BaseURL + "api/CustomersEmployeeAPI/AddUsers";

        public static string AddFrequentFlyer = BaseURL + "api/FrequentFlyerAPI/AddFrequentFlyer";

        public static string GetFrequentFlyer = BaseURL + "api/FrequentFlyerAPI/GetFrequentFlyer";

        public static string UpdateFrequentFlyer = BaseURL + "api/FrequentFlyerAPI"; 

        public static string AddInternalusers = BaseURL + "api/InternalUsersAPI/AddInternalUsers";
        public static string GetInternalusers = BaseURL + "api/InternalUsersAPI/GetInternalUsers";

        public static string GetInternalusersID = BaseURL + "api/InternalUsersAPI";

        public static string AddContactUS = BaseURL + "api/ContactUsDetailsAPI/AddContactUs";
        public static string AddBillingEntity = BaseURL + "api/EmployeeBillingEntityAPI/AddBillingEntity";
        public static string AddLoactionTax = BaseURL + "api/LocationsandTaxAPI/AddLocationsandTax";
        public static string GetLoactionTax = BaseURL + "api/LocationsandTaxAPI/GetLocationsandTax";
        public static string GetLoactionTaxAll = BaseURL + "api/LocationsandTaxAPI/GetLocationsandTaxALL";
        public static string GetLoactionTaxID = BaseURL + "api/LocationsandTaxAPI";
        public static string EditLoactionTaxID = BaseURL + "api/LocationsandTaxAPI";
        public static string GetCountry = BaseURL + "api/LocationAPI/countries";
        public static string GetSate = BaseURL + "api/LocationAPI/states";
        public static string GetSatebylegalentity = BaseURL + "api/LocationAPI/legalEntitystate";
        public static string GetCity = BaseURL + "api/LocationAPI/cities";
        public static string AddSupplierCredential = BaseURL + "api/SuppliersCredentialAPI/addsuppliercred";
        public static string GetSupplierCredential = BaseURL + "api/SuppliersCredentialAPI/getsuppliercred";
        public static string GetSupplierCredentialID = BaseURL + "api/SuppliersCredentialAPI";
        public static string AddDealCode = BaseURL + "api/DealCodeAPI/AddDealCode";
        public static string AddcustomerDealCode = BaseURL + "api/DealCodeAPI/AddcustomerDealCode";
        public static string GetDealCode = BaseURL + "api/DealCodeAPI/GetDealCode";
        public static string GetDealCodeID = BaseURL + "api/DealCodeAPI";
        public static string GetDealCodeSupplierId = BaseURL + "api/DealCodeAPI/GetDealCodeSupplierId";
        public static string GetcustomerDealCode = BaseURL + "api/DealCodeAPI/GetcustomerDealCode";
        public static string GetCustomerDepartment = BaseURL + "api/CustomerDepartmentAPI/GetAllCustomerDepartment";
        public static string AddCustomerDepartment = BaseURL + "api/CustomerDepartmentAPI/AddCustomerDepartment";
        public static string GetCustomerDesignation = BaseURL + "api/CustomerDesignationAPI/GetAllCustomerDesignation";
        public static string AddCustomerDesignation = BaseURL + "api/CustomerDesignationAPI/AddCustomerDesignation";

        #endregion

    }
}
