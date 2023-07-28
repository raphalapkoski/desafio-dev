using AutoMapper;
using Bogus;
using DesafioDev.Api.Controllers;
using DesafioDev.Application.Features.File;
using DesafioDev.Application.Requests;
using DesafioDev.Application.Response;
using DesafioDev.Tests.Extensions;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace DesafioDev.Tests.Controllers
{
    public class FileControllerTests
    {
        readonly Mock<ISender> _sender;
        readonly Mock<IMapper> _mapper;
        public FileController _fileController;

        public FileControllerTests()
        {
            _sender = new Mock<ISender>();
            _mapper = new Mock<IMapper>();
            _fileController = new FileController(_mapper.Object, _sender.Object);
        }

        [Fact]
        public async Task Upload_Return_200_Ok()
        {
            var uploadFileCommand = CreateUploadFileRequest().Generate();

            _sender.Setup(_ => _.Send(It.IsAny<UploadFileCommand>(), It.IsAny<CancellationToken>()))
                   .ReturnsAsync(new BaseResponse<string>(true, "Success", null));

            var result = await _fileController.Upload(uploadFileCommand);

            result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Fact]
        public async Task Upload_Return_400_Bad_Request()
        {
            var uploadFileCommand = CreateUploadFileRequest().Generate();

            _sender.Setup(_ => _.Send(It.IsAny<UploadFileCommand>(), It.IsAny<CancellationToken>()))
                   .ReturnsAsync(new BaseResponse<string>(false, null, new List<Error>
                   {
                       new Error("Ocorreu uma falha ao ler o aquivo, verifique o padrão do documento enviado e tente novamente.")
                   }));

            var result = await _fileController.Upload(uploadFileCommand);

            result.Should().BeOfType<BadRequestObjectResult>().Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Upload_Return_500_Internal_Server_Error()
        {
            var uploadFileCommand = CreateUploadFileRequest().Generate();

            _sender.Setup(_ => _.Send(It.IsAny<UploadFileCommand>(), It.IsAny<CancellationToken>()))
                   .Throws(new Exception());
            
            var result = await _fileController.Upload(uploadFileCommand);

            result.Should().BeOfType<StatusCodeResult>().Which.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        }

        private static Faker<UploadFileRequest> CreateUploadFileRequest()
        {
            return new Faker<UploadFileRequest>().WithRecord().RuleFor(_ => _.File, _ => new Mock<IFormFile>().Object);
        }        
    }
}
