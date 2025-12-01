using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace JetwaysAdmin.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuRightsController : ControllerBase
    {
        private readonly IMenuRightsRepository _menurights;

        public MenuRightsController(IMenuRightsRepository menurights)
        {
            _menurights = menurights;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<MenuRightDto>>> GetUserRights(int userId)
        {
            var rights = await _menurights.GetUserRightsAsync(userId);
            return Ok(rights);
        }

        [HttpPost("Menurightssave")]
        public async Task<IActionResult> SaveUserRights([FromBody] UserMenuRightsDto model)
        {
            if (model == null || model.Rights == null)
                return BadRequest("Invalid data");

            await _menurights.SaveUserRightsAsync(model);
            return Ok(new { success = true });
        }
    }
}
