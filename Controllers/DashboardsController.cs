using ApiSalesCrud.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiSalesCrud.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardsController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardsController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {   
            var dashboard = await _dashboardService.GetDatas();
            return Ok(dashboard);
        }
    }
}