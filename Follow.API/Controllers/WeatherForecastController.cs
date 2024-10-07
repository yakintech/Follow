using Microsoft.AspNetCore.Mvc;

namespace Follow.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        [HttpGet]
        public string[] Get()
        {
            var names = new string[] { "John", "Doe" };
            return names;
        }
    }
}
