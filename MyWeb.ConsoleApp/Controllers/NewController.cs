using Microsoft.AspNetCore.Mvc;

namespace MyWeb.ConsoleApp.Controllers
{
    [ApiController]
    [Route("NewApi/[action]")]
    public class NewController:ControllerBase
    {
        [HttpGet]
        public string SayHi()
        {
            // localhost:xxxx/NewApi/SayHi
            return "Hi Learners ...";
        }
    }
}
