using Microsoft.AspNetCore.Mvc;
using SendEmail.Domain.Services;

namespace SendEmail.Web.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class SendEmailController : ControllerBase
{
    [HttpPost]
    public IActionResult SendEmail()
    {
        SmtpServices.SendEmailAsync();
        return Ok();
    }
}