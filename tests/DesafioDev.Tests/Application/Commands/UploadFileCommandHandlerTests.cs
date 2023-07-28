using Bogus;
using Bogus.Extensions.Brazil;
using DesafioDev.Application.Features.File;
using DesafioDev.Application.Interfaces;
using DesafioDev.Application.Response;
using DesafioDev.Domain.Entities;
using DesafioDev.Domain.Enums;
using DesafioDev.Domain.Repositories;
using DesafioDev.Tests.Extensions;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;

namespace DesafioDev.Tests.Application.Commands
{
    public class UploadFileCommandHandlerTests
    {
        readonly Mock<IUnitOfWork> _unitOfWork;
        readonly Mock<IFileServices> _fileServices;
        readonly UploadFileCommandHandler _uploadFileCommandHandler;

        public UploadFileCommandHandlerTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _fileServices = new Mock<IFileServices>();
            _uploadFileCommandHandler = new UploadFileCommandHandler(_unitOfWork.Object, _fileServices.Object);
        }

        [Fact]
        public async Task Handle_Upload_File_Success()
        {
            var command = CreateUploadFileCommand().Generate();
            var establishments = CreateCollectionEstablishment().Generate(5);

            _fileServices.Setup(_ => _.DesserializeValuesForEstablishment(It.IsAny<IFormFile>()))
                         .Returns(establishments);

            _unitOfWork.Setup(_ => _.EstablishmentRepository.SaveRangeAsync(It.IsAny<ICollection<Establishment>>()))
                       .Returns(Task.CompletedTask);

            var result = await _uploadFileCommandHandler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull()
                  .And.BeOfType<BaseResponse<string>>()
                  .Which.Success.Should().BeTrue();

            _fileServices.Verify(_ => _.DesserializeValuesForEstablishment(It.IsAny<IFormFile>()), Times.Once);
            _unitOfWork.Verify(_ => _.EstablishmentRepository.SaveRangeAsync(It.IsAny<ICollection<Establishment>>()), Times.Once);
            _unitOfWork.Verify(_ => _.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_Upload_File_Not_Success_When_It_Fails_To_Read_File()
        {
            var command = CreateUploadFileCommand().Generate();
            
            _fileServices.Setup(_ => _.DesserializeValuesForEstablishment(It.IsAny<IFormFile>()))
                         .Returns(new List<Establishment>());
                      
            var result = await _uploadFileCommandHandler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull()
                  .And.BeOfType<BaseResponse<string>>()
                  .Which.Success.Should().BeFalse();

            _fileServices.Verify(_ => _.DesserializeValuesForEstablishment(It.IsAny<IFormFile>()), Times.Once);
            _unitOfWork.Verify(_ => _.EstablishmentRepository.SaveRangeAsync(It.IsAny<ICollection<Establishment>>()), Times.Never);
            _unitOfWork.Verify(_ => _.CommitAsync(), Times.Never);
        }

        private static Faker<UploadFileCommand> CreateUploadFileCommand()
        {
            return new Faker<UploadFileCommand>()
                       .WithRecord()
                       .RuleFor(_ => _.File, _ => new Mock<IFormFile>().Object);
        }

        private static Faker<Establishment> CreateCollectionEstablishment()
        {
            return new Faker<Establishment>()
                       .WithRecord()
                       .RuleFor(_ => _.Name, _ => _.Company.CompanyName())
                       .RuleFor(_ => _.Owner, _ => new Owner(_.Person.Cpf(), _.Person.FullName))
                       .RuleFor(_ => _.Transactions, _ => new List<Transaction>
                       {
                           new Transaction((TransactionType)_.Random.Int(1, 9), _.Date.Past(5), _.Random.Decimal(0, 2000), _.Name.Random.ToString(), _.Date.Timespan())
                       });

        }
    }
}
