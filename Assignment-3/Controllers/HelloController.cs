using Assignment_3.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Assignment_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloController : ControllerBase
    {
        private readonly IHelloSerivice _helloSerivice;
        public HelloController(IHelloSerivice helloSerivice)
        {
            _helloSerivice = helloSerivice;
        }

        [EnableRateLimiting("Fixed")]
        [HttpGet("hello")]
        public IActionResult GetHello()
        {
            var message = _helloSerivice.GetMessage();
            return Ok(message);
        }
    }
}
