using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]  // Client needs to specify api/Users/<method name>

    public class BaseApiController : ControllerBase
    {

    }
}