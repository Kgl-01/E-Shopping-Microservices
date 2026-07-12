using Microsoft.AspNetCore.Mvc;

namespace UserManagementService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NameController : ControllerBase
    {
        [HttpGet]
        [Route("/user/signup")]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }
    }
}
