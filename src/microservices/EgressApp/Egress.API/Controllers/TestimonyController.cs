using Egress.Application.Commands.Testimony;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Egress.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class TestimonyController : ControllerBase
{
    private readonly IMediator _mediator;

    public TestimonyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateTestimonyCommand command)
    {
        var result = _mediator.Send(command);
        return Ok(result);
    }
}