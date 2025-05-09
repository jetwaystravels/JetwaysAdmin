﻿namespace JetwaysAdmin.UI.ApplicationUrl
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

        public static string AddAccountBalance = BaseURL + "api/AccountBalanceAPI/AccountBalance";

        public static string ManageStaff = BaseURL + "api/ManageStaffAPI/ManageStaff";

        public static string Dashboard = BaseURL + "api/DashboardAPI/Dashboard";

        #endregion

    }
}
