using JetwaysAdmin.Entity;
using JetwaysAdmin.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JetwaysAdmin.WebAPI.Controllers
{

   


    [Route("api/[controller]")]
    [ApiController]
    public class DashboardAPIController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public DashboardAPIController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
       

        [HttpGet]
        [Route("Dashboard")]
        public async Task<ActionResult<DashboardSummary>> GetDashboardSummary()
        {
            var result = await _appDbContext.Admin_tb_LegalEntity.CountAsync();
            return Ok(result);
        }

       


    }


}
