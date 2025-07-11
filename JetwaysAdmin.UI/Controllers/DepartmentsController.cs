﻿using JetwaysAdmin.UI.ApplicationUrl;
using JetwaysAdmin.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JetwaysAdmin.UI.Controllers
{
    public class DepartmentsController : Controller
    {
        public IActionResult ShowDepartments(int Id, string LegalEntityCode, string LegalEntityName)
        {
            ViewBag.LegalEntityCode = LegalEntityCode;
            ViewBag.LegalEntityName = LegalEntityName;
            ViewBag.Id = Id;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddDepartment()
        {
            return View();
        }

    }
}
