using DesafioDev.Api.Documentation;
using DesafioDev.Application.Features.Establishment;
using DesafioDev.Application.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DesafioDev.Api.Controllers;

[Route("api/establishment")]
[ApiController]
public class EstablishmentController : ControllerBase
{
    readonly ISender _sender;

    public EstablishmentController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    [Produces("application/json")]
    [SwaggerOperation(Summary = EstablishmentControllerDocumentation.Summary, Description = EstablishmentControllerDocumentation.Description)]
    [ProducesResponseType(typeof(IEnumerable<EstablishmentQueryResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var result = await _sender.Send(new EstablishmentQuery());
            return result.Any() ? Ok(result) : NotFound();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return StatusCode(500);
        }
    }
}
