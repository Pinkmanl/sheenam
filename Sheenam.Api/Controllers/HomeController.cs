using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace Sheenam.Api.Controllers
{
    [ApiController] 
    [Route("api/[controller]")]
    public class HomeController : RESRFulController
    {
 
        [HttpGet]
        public string Get() => "Hello Pinkmanl!";

    }
}
