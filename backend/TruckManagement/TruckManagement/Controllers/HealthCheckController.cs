using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using TruckManagement.ViewModels;

namespace TruckManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(ResultViewModel), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            return Ok(new ResultViewModel
            {
                Error = null,
                Message = $"It's working fine at {DateTime.UtcNow}",
                Success = true
            });
        }
    }
}
