using MediatR;
using Microsoft.AspNetCore.Mvc;
using SendEmail.Application.Emails.Commands;
using SendEmail.Application.Emails.Dtos;
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
    /// <summary>
    /// Obtem os e-mails registrados 
    /// </summary>
    /// <returns>Email</returns>
    /// <response code="200">200 Sucesso</response>
    /// <response code="400">400 Erro</response>
    [HttpGet]
    public async Task<IActionResult> GetListAsync()
    {
        var emailListQuery = await mediator.Send(new GetEmailListQuery());

        return Ok(emailListQuery);
    }

    /// <summary>
    /// Realiza o envio de um e-mail
    /// </summary>
    /// <returns>Email</returns>
    /// <response code="200">200 Sucesso</response>
    /// <response code="400">400 Erro</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpPost]
    public async Task<IActionResult> SendEmail([FromBody] SendEmailCommand values)
    {
        var result = await mediator.Send(values);
        return Ok(result);
    }
}