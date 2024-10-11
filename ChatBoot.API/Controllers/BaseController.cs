using Microsoft.AspNetCore.Mvc;

namespace OrderManagementService.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BaseController : ControllerBase
    {
               
    }
}
