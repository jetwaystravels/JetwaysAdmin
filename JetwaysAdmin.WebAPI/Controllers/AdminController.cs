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
        private readonly IInternalUsers<InternalUsers> _internalUsers;
        public AdminController(IAdmin<Admin> admin, IInternalUsers<InternalUsers> internalUsers)
        {
            this._admin = admin;
            _internalUsers = internalUsers;
        }


        [HttpGet("LogIn")]
        public async Task<IActionResult> Login()
        {
           var data= await _admin.GetAllAsync();
            return Ok(data);

            //return View();

        }


        //[Route("LogIn")]
       
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // _admin.Login returns Task<Admin>
                var admin = await _admin.Login(loginRequest.Username, loginRequest.Password);

                if (admin == null) // wrong username/password
                    return Unauthorized(new { message = "Invalid username or password." });

                return Ok(new { id = admin.admin_id, name = admin.admin_name });
            }
            catch (Exception ex)
            {
                // optional: _logger.LogError(ex, "Login failed for {User}", loginRequest.Username);
                return StatusCode(500, new { message = "Something went wrong." });
            }
        }
        [HttpPost("CorporateLogIn")]
        public async Task<IActionResult> CorporateLogIn([FromBody] CoprateLoginRequest CoprateloginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _internalUsers.LoginAsync(CoprateloginRequest.BusinessEmail, CoprateloginRequest.Password);

            if (user != null)
            {
                return Ok(new
                {
                    user.UserID,
                    user.FirstName,
                    user.LastName,
                    user.BusinessEmail,
                    user.MobileNumber,
                    user.UserType
                });
            }
            else
            {
                return Unauthorized(new { message = "Invalid email or password." });
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
