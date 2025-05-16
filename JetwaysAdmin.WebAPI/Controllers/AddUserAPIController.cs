using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using JetwaysAdmin.Repositories.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JetwaysAdmin.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddUserAPIController : ControllerBase
    {
        private readonly IAddUser<AddUser> _user;

        public AddUserAPIController(IAddUser<AddUser> user)
        {
            this._user = user;
        }



        [HttpPost]
        [Route("AddUser")]
        public async Task<ActionResult> AddUser([FromBody] AddUser addUser) 
        {
            if (addUser == null) {

                return BadRequest("Invalid data.");
            }

            await _user.AddUser(addUser);
            return Ok(new { message = "User added successfully!" });
        }



        [HttpGet]
        [Route("GetManageUser")]
        public async Task<ActionResult<IEnumerable<AddUser>>> GetManageUser()
        {
            var getManageUser = await _user.GetManageUser();
            return Ok(getManageUser);
        }


    }
}
