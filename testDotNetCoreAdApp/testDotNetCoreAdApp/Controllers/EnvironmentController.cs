using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace testDotNetCoreAdApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnvironmentController : ControllerBase
    {
        private readonly IConfiguration Configuration;

        public EnvironmentController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // GET api/environment
        [HttpGet]
        public ActionResult<string> Get()
        {
            return Configuration.GetValue<string>("Environment");
        }
    }
}