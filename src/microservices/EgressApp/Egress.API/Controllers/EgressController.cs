using Microsoft.AspNetCore.Mvc;

namespace Egress.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class EgressController : ControllerBase
{
    public EgressController()
    {
        
    }
}