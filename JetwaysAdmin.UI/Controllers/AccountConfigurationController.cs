﻿using JetwaysAdmin.Entity;
using JetwaysAdmin.UI.ApplicationUrl;
using JetwaysAdmin.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JetwaysAdmin.UI.Controllers
{
    public class AccountConfigurationController : Controller
    {
        public async Task<IActionResult> ShowAccountConfiguration()
        {
            return View();
        }
    }
}
