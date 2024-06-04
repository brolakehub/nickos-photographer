using Microsoft.AspNetCore.Mvc;

namespace BackEnd_App.Controllers
{
    [ApiController]
    [Route("/env")]
    public class EnvSystemController : ControllerBase
    {
        [HttpGet]
        public string Index()
        {
            return Environment.GetEnvironmentVariable("testENV");
        }
    }
}
