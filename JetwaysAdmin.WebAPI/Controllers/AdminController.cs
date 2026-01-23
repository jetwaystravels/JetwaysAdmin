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
        private readonly EncryptionService _encryption;
        private readonly IPassword _passwordService;
        public AdminController(
        IAdmin<Admin> admin,
        IInternalUsers<InternalUsers> internalUsers,
        EncryptionService encryption, IPassword passwordService)
            {
                _admin = admin;
                _internalUsers = internalUsers;
                _encryption = encryption;
                _passwordService = passwordService;
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

            var admins = await _admin.GetAllAsync();
            var admin = admins.FirstOrDefault(a =>
                a.admin_email != null &&
                a.admin_email.Equals(loginRequest.Username, StringComparison.OrdinalIgnoreCase));

            if (admin == null)
                return Unauthorized(new { message = "Invalid username or password." });

            //var input = (loginRequest.Password ?? "").Trim();


            var input = loginRequest.Password ?? "";
            var stored = admin.admin_password ?? "";

            var debug = new
            {
                emailFound = admin.admin_email,
                inputLength = input.Length,
                storedLength = stored.Length,
                storedPrefix = stored.Length >= 4 ? stored.Substring(0, 4) : stored,
                isBcrypt = stored.StartsWith("$2", StringComparison.Ordinal),
                verifyResult = BCrypt.Net.BCrypt.Verify(input, stored)
            };

            return Ok(debug);

            // ✅ 1) New system: verify hash
            //if (!string.IsNullOrWhiteSpace(admin.admin_password))
            //{
            //    if (!_passwordService.Verify(input, admin.admin_password))
            //        return Unauthorized(new { message = "Invalid username or password." });

            //    return Ok(new
            //    {
            //        id = admin.admin_id,
            //        name = admin.admin_name,
            //        email = admin.admin_email,
            //        userType = string.Equals(admin.admin_name, "SuperAdmin", StringComparison.OrdinalIgnoreCase)
            //            ? "SuperAdmin"
            //            : "Admin"
            //    });
            //}


            // If no hash and no old password => invalid setup
            return Unauthorized(new { message = "Invalid username or password." });
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

            //admin.admin_password = _encryption.Encrypt(admin.admin_password);
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
