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
        private readonly EncryptionService _encryption;
        public AdminController(
        IAdmin<Admin> admin,
        IInternalUsers<InternalUsers> internalUsers,
        EncryptionService encryption)
            {
                _admin = admin;
                _internalUsers = internalUsers;
                _encryption = encryption;
            }
      

        [HttpGet("LogIn")]
        public async Task<IActionResult> Login()
        {
           var data= await _admin.GetAllAsync();
            return Ok(data);

            //return View();

        }


        //[Route("LogIn")]

        //[HttpPost("Login")]
        //public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    try
        //    {
        //        // _admin.Login returns Task<Admin>
        //        var admin = await _admin.Login(loginRequest.Username, loginRequest.Password);

        //        if (admin == null) // wrong username/password
        //            return Unauthorized(new { message = "Invalid username or password." });

        //        return Ok(new { id = admin.admin_id, name = admin.admin_name });
        //    }
        //    catch (Exception ex)
        //    {
        //        // optional: _logger.LogError(ex, "Login failed for {User}", loginRequest.Username);
        //        return StatusCode(500, new { message = "Something went wrong." });
        //    }
        //}
       
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                
                var admins = await _admin.GetAllAsync();
                var admin = admins.FirstOrDefault(a => a.admin_email == loginRequest.Username);

                if (admin == null)
                    return Unauthorized(new { message = "Invalid username or password." });
                string decryptedPassword;
                try
                {
                   decryptedPassword = _encryption.Decrypt(admin.admin_password);
                    if (!string.IsNullOrEmpty(decryptedPassword) &&
                        decryptedPassword.StartsWith("CfDJ8", StringComparison.Ordinal))
                    {
                        decryptedPassword = _encryption.Decrypt(decryptedPassword);
                    }
                }
                catch
                {
                    decryptedPassword = admin.admin_password;
                }

                var input = (loginRequest.Password ?? "").Trim();
                var decrypted = (decryptedPassword ?? "").Trim();

                if (decrypted != input)
                {
                    return Unauthorized(new
                    {
                        message = "Password mismatch",
                        inputPassword = input,
                        decryptedPassword = decrypted
                    });
                }

                return Ok(new
                {
                    id = admin.admin_id,
                    name = admin.admin_name,
                    email = admin.admin_email,
                    userType = admin.admin_name == "SuperAdmin" ? "SuperAdmin" : "Admin"
                });
            }
            catch
            {
                return StatusCode(500, new { message = "Something went wrong." });
            }
        }

        [HttpPost("CorporateLogIn")]
        public async Task<IActionResult> CorporateLogIn([FromBody] CoprateLoginRequest CoprateloginRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var email = (CoprateloginRequest.BusinessEmail ?? "").Trim();
                var inputPassword = (CoprateloginRequest.Password ?? "").Trim();
                var users = await _internalUsers.GetInternalUsers();
                var user = users.FirstOrDefault(u =>
                    u.BusinessEmail != null &&
                    u.BusinessEmail.Trim().Equals(email, StringComparison.OrdinalIgnoreCase));

                if (user == null)
                    return Unauthorized(new { message = "Invalid email or password." });
                string decryptedPassword;
                try
                {
                    decryptedPassword = _encryption.Decrypt(user.Password);

                    if (!string.IsNullOrEmpty(decryptedPassword) &&
                        decryptedPassword.StartsWith("CfDJ8", StringComparison.Ordinal))
                    {
                        decryptedPassword = _encryption.Decrypt(decryptedPassword);
                    }
                }
                catch
                {
                    decryptedPassword = user.Password ?? "";
                }

                if (!string.Equals(
                        (decryptedPassword ?? "").Trim(),
                        inputPassword,
                        StringComparison.Ordinal))
                {
                    return Unauthorized(new { message = "Invalid email or password." });
                }

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
            catch
            {
                return StatusCode(500, new { message = "Something went wrong." });
            }
        }

        //[HttpPost("CorporateLogIn")]
        //public async Task<IActionResult> CorporateLogIn([FromBody] CoprateLoginRequest CoprateloginRequest)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var user = await _internalUsers.LoginAsync(CoprateloginRequest.BusinessEmail, CoprateloginRequest.Password);

        //    if (user != null)
        //    {
        //        return Ok(new
        //        {
        //            user.UserID,
        //            user.FirstName,
        //            user.LastName,
        //            user.BusinessEmail,
        //            user.MobileNumber,
        //            user.UserType
        //        });
        //    }
        //    else
        //    {
        //        return Unauthorized(new { message = "Invalid email or password." });
        //    }

        //}


        //[HttpPost("AddAdmin")]
        //public async Task AddAdmin(Admin admin)
        //{
        //    await _admin.AddAsync(admin);
        //}
        [HttpPost("AddAdmin")]
        public async Task<IActionResult> AddAdmin([FromBody] Admin admin)
        {
            if (admin == null) return BadRequest();

            admin.admin_password = _encryption.Encrypt(admin.admin_password);

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
