using AutoMapper;
using DesafioDev.Api.Documentation;
using DesafioDev.Application.Features.File;
using DesafioDev.Application.Requests;
using DesafioDev.Application.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DesafioDev.Api.Controllers;

[Route("api/file")]
[ApiController]
public class FileController : ControllerBase
{
    readonly IMapper _mapper;
    readonly ISender _sender;

    public FileController(IMapper mapper, ISender sender)
    {
        _mapper = mapper;
        _sender = sender;
    }

    [HttpPost("upload")]
    [Consumes("multipart/form-data")]
    [Produces("application/json")]
    [SwaggerOperation(FileControllerDocumentation.Summary, FileControllerDocumentation.Description)]
    [ProducesResponseType(typeof(BaseResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Upload([FromForm] UploadFileRequest uploadFileRequest)
    {
        try
        {
            var result = await _sender.Send(_mapper.Map<UploadFileCommand>(uploadFileRequest));
            return result.Success ? Ok(result) : BadRequest(result);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return StatusCode(500);
        }
    }
 }
