using AutoMapper;
using DesafioDev.Application.Features.File;
using DesafioDev.Application.Requests;

namespace DesafioDev.Application.Mappers;

public class RequestToCommandMapping : Profile
{
    public RequestToCommandMapping()
    {
        CreateMap<UploadFileRequest, UploadFileCommand>();
    }
}
