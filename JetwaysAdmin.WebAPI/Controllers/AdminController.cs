using JetwaysAdmin.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using JetwaysAdmin.WebAPI.Models;
using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Implementations;

namespace JetwaysAdmin.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly IAdmin<Admin> _admin;
        private readonly IInternalUsers<InternalUsers> _internalUsers;
        //private readonly EncryptionService _encryption;
        private readonly IPassword _passwordService;
        public AdminController(
        IAdmin<Admin> admin,
        IInternalUsers<InternalUsers> internalUsers,
         IPassword passwordService)
            {
                _admin = admin;
                _internalUsers = internalUsers;
               // _encryption = encryption;
                _passwordService = passwordService;
        }
      

        [HttpGet("LogIn")]
        public async Task<IActionResult> Login()
        {
           var data= await _admin.GetAllAsync();
            return Ok(data);

            //return View();

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var admins = await _admin.GetAllAsync();
            var admin = admins.FirstOrDefault(a =>
                a.admin_email != null &&
                a.admin_email.Equals(loginRequest.Username, StringComparison.OrdinalIgnoreCase));

            if (admin == null)
                return Unauthorized(new { message = "Invalid username or password." });
            var input = loginRequest.Password ?? "";
            var stored = admin.admin_password ?? "";
            if (!string.IsNullOrWhiteSpace(admin.admin_password))
            {
                if (!_passwordService.Verify(input, admin.admin_password))
                    return Unauthorized(new { message = "Invalid username or password." });

                return Ok(new
                {
                    id = admin.admin_id,
                    name = admin.admin_name,
                    email = admin.admin_email,
                    userType = string.Equals(admin.admin_name, "SuperAdmin", StringComparison.OrdinalIgnoreCase)
                        ? "SuperAdmin"
                        : "Admin"
                });
            }

            return Unauthorized(new { message = "Invalid username or password." });
        }



        [HttpPost("CorporateLogIn")]
        public async Task<IActionResult> CorporateLogIn([FromBody] CoprateLoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var email = (request.BusinessEmail ?? "").Trim();
                var inputPassword = request.Password ?? "";

                var users = await _internalUsers.GetInternalUsers();
                var user = users.FirstOrDefault(u =>
                    !string.IsNullOrWhiteSpace(u.BusinessEmail) &&
                    u.BusinessEmail.Trim().Equals(email, StringComparison.OrdinalIgnoreCase));

                if (user == null)
                    return Unauthorized(new { message = "Invalid email or password." });

                var storedPassword = user.Password ?? "";

              
                if (!string.IsNullOrWhiteSpace(storedPassword))
                {
                    if (!_passwordService.Verify(inputPassword, storedPassword))
                        return Unauthorized(new { message = "Invalid email or password." });

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

                return Unauthorized(new { message = "Invalid email or password." });
            }
            catch
            {
                return StatusCode(500, new { message = "Something went wrong." });
            }
        }


        [HttpPost("AddAdmin")]
        public async Task<IActionResult> AddAdmin([FromBody] Admin admin)
        {
            if (admin == null) return BadRequest();
            admin.admin_password = BCrypt.Net.BCrypt.HashPassword(admin.admin_password);
            await _admin.AddAsync(admin);
            return Ok(new { message = "Admin added successfully" });
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
