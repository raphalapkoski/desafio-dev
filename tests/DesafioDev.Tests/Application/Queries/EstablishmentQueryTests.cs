using Bogus;
using Bogus.Extensions.Brazil;
using DesafioDev.Application.Features.Establishment;
using DesafioDev.Domain.Entities;
using DesafioDev.Domain.Enums;
using DesafioDev.Domain.Repositories;
using DesafioDev.Tests.Extensions;
using FluentAssertions;
using Moq;

namespace DesafioDev.Tests.Application.Queries
{
    public class EstablishmentQueryTests
    {
        readonly Mock<IUnitOfWork> _unitOfWork;
        readonly EstablishmentQueryHandler _establishmentQueryHandler;

        public EstablishmentQueryTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _establishmentQueryHandler = new EstablishmentQueryHandler(_unitOfWork.Object);
        }

        [Fact]
        public async Task Handle_Get_Informations_Establishment_Success()
        {
            var establishments = CreateCollectionEstablishment().Generate(5);
            _unitOfWork.Setup(_ => _.EstablishmentRepository.GetAllAsync())
                       .ReturnsAsync(establishments);

            var result = await _establishmentQueryHandler.Handle(It.IsAny<EstablishmentQuery>(), CancellationToken.None);

            result.Should().NotBeNull();
            _unitOfWork.Verify(_ => _.EstablishmentRepository.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_Get_Informations_Establishment_Is_Empty()
        {
            _unitOfWork.Setup(_ => _.EstablishmentRepository.GetAllAsync())
                       .ReturnsAsync(Enumerable.Empty<Establishment>);

            var result = await _establishmentQueryHandler.Handle(It.IsAny<EstablishmentQuery>(), CancellationToken.None);

            result.Should().BeNullOrEmpty();
            _unitOfWork.Verify(_ => _.EstablishmentRepository.GetAllAsync(), Times.Once);
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
