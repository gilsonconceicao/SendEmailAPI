using MediatR;
using Microsoft.AspNetCore.Mvc;
using SendEmail.Application.Emails.Queries;

namespace SendEmail.Web.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class SendEmailController : ControllerBase
{
    private readonly IMediator mediator;

    public SendEmailController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetListAsync()
    {
        var emailListQuery = await mediator.Send(new GetEmailListQuery());

        return Ok(emailListQuery); 
    }

    // [HttpPost]
    // public IActionResult SendEmail()
    // {
    //     SmtpServices.SendEmailAsync();
    //     return Ok();
    // }
}