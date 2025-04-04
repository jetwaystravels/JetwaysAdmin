using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace JetwaysAdmin.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : Controller
    {
        private readonly IMenu<MenuItem> _menu;
        public MenuController(IMenu<MenuItem> menu)
        {
            this._menu = menu;
        }


        [HttpGet("MenuList")]
        public async Task<IActionResult> MenuList()
        {
            var data = await _menu.GetAllAsync();
            return Ok(data);

            //return View();

        }
    }
}
