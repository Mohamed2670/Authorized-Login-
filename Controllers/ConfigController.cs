using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MwTesting.Controllers
{
    [ApiController]
    [Route("api/config")]
    [Authorize]

    public class ConfigController : ControllerBase
    {
        private readonly IConfiguration _config;
        public ConfigController(IConfiguration config)
        {
            _config = config;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var item = new
            {
                AllowedHosts = _config["AllowedHosts"],
                log = _config["Logging:LogLevel:Default"]
            };
            return Ok(item);
        }
    }
}