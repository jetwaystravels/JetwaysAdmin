using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories.Interface;
using JetwaysAdmin.Repositories.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JetwaysAdmin.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IATAGruopAPIController : ControllerBase
    {
        private readonly IIATAGroup<IATAGroup> _IATAgroup;

         public IATAGruopAPIController(IIATAGroup<IATAGroup> IATAgroup)
        {
            _IATAgroup = IATAgroup;
        }

        [HttpGet]
        [Route("GetIATAGroup")]
        public async Task<ActionResult<List<IATAGroup>>> GetIATAGroup()
        {
            return Ok(await _IATAgroup.GetIATAGruop());
        }
    }
}
