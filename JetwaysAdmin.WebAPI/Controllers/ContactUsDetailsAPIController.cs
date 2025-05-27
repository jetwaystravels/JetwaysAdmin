using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JetwaysAdmin.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsDetailsAPIController : ControllerBase
    {
        private readonly IContactUsDetails<ContactUsDetails> _contactus;
        public ContactUsDetailsAPIController(IContactUsDetails<ContactUsDetails> contactus)
        {
            this._contactus= contactus;
        }
        [HttpPost]
        [Route("AddContactUs")]
        public async Task<ActionResult> AddContactUs([FromBody] ContactUsDetails addContactus)
        {
            if (addContactus == null)
            {

                return BadRequest("Invalid data.");
            }
            await _contactus.AddContactUs(addContactus);
            return Ok(new { message = "User added successfully!" });
        }

    }
}
