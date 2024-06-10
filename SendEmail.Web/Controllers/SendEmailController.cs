using Microsoft.AspNetCore.Mvc;

namespace SendEmail.Web.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class SendEmailController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }
}