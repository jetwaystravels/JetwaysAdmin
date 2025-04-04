using JetwaysAdmin.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using JetwaysAdmin.WebAPI.Models;
using JetwaysAdmin.Entity;

namespace JetwaysAdmin.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly IAdmin<Admin> _admin;
        public AdminController(IAdmin<Admin> admin)
        {
            this._admin = admin;
        }


        [HttpGet("LogIn")]
        public async Task<IActionResult> Login()
        {
           var data= await _admin.GetAllAsync();
            return Ok(data);

            //return View();

        }


        //[Route("LogIn")]
        [HttpPost("LogIn")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return validation errors
            }

            var admin = _admin.Login(loginRequest.Username, loginRequest.Password);

            if (admin != null)
            {
                // Optionally set session or token here
                return Ok(new { message = loginRequest.Username });
            }
            else
            {
                return Unauthorized(new { message = "Invalid username or password." });
            }
        }

        [HttpPost("AddAdmin")]
        public async Task AddAdmin(Admin admin)
        {
            await _admin.AddAsync(admin);
        }
        [HttpGet("GetAdminID")]
        public async Task<Admin> GetAdminById(int userID)
        {
            return await _admin.GetByIdAsync(userID);
        }

        [HttpPost("UpdateAdmin")]
        public async Task UpdateAdmin(Admin admin)
        {
            await _admin.UpdateAsync(admin);
        }

        [HttpPost("DeleteAdmin")]
        public async Task DeleteAdmin(int id)
        {
            await _admin.DeleteAsync(id);
        }


    }
}
