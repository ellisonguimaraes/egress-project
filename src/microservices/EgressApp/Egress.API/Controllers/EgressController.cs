using Egress.API.Models;
using Egress.Application.Queries.Person.GetPersonByDocument;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Egress.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class EgressController : ControllerBase
{
    private readonly IMediator _mediator;

    public EgressController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetByDocumentAsync([FromQuery] GetPersonByDocumentCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new GenericHttpResponse
        {
            Data = result
        });
    }
}