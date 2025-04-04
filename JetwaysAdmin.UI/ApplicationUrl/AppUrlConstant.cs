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

        #endregion

    }
}
