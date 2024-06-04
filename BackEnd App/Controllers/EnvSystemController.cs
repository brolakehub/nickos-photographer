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
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT");
            return Environment.GetEnvironmentVariable("testENV");
        }
    }
}
