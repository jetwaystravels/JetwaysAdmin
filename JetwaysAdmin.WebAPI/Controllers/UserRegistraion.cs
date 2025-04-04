using JetwaysAdmin.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JetwaysAdmin.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRegistraion : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        public UserRegistraion(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet("Registraion")]
        public IActionResult Registraion()
        {
            return View();
        }

        [HttpPost("Registraion")]
        public async Task<IActionResult> Registraion(Registration registration)
        {
            var user = new IdentityUser
            {
                UserName = registration.UserName,
                Email = registration.Email,
                PhoneNumber = registration.PhoneNumber
            };


            var result = await userManager.CreateAsync(user, registration.Password);

            // If user is successfully created, sign-in the user using
            // SignInManager and redirect to index action of HomeController
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: false);
                return Ok(registration);
            }

            return View();
        }
    }
}
