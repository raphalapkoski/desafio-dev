using DesafioDev.Application.Abstractions.Command;
using DesafioDev.Application.Response;
using Microsoft.AspNetCore.Http;

namespace DesafioDev.Application.Features.File;

public sealed record UploadFileCommand(IFormFile File) : ICommand<BaseResponse<string>>;
