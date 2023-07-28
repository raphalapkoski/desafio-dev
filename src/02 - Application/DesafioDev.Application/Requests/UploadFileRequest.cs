using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace DesafioDev.Application.Requests;

public sealed record UploadFileRequest([Required(ErrorMessage = "Selecione um arquivo para fazer o upload")] IFormFile File);
