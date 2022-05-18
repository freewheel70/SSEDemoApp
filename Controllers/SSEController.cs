using Microsoft.AspNetCore.Mvc;

namespace SSEDemoApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SSEController : ControllerBase
    {

        private readonly ILogger<SSEController> _logger;

        public SSEController(ILogger<SSEController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task Get()
        {
            await HttpContext.SSEInitAsync();
            for (int i = 0; i < 10; i++)
            {
                await HttpContext.SSESendDataAsync();
            }
            await HttpContext.SSESendCloseEventAsync();
        }


    }
}