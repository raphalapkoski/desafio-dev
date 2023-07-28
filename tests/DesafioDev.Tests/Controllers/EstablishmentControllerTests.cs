using Bogus;
using Bogus.Extensions.Brazil;
using DesafioDev.Api.Controllers;
using DesafioDev.Application.Features.Establishment;
using DesafioDev.Application.Response;
using DesafioDev.Tests.Extensions;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace DesafioDev.Tests.Controllers;

public class EstablishmentControllerTests
{

    readonly Mock<ISender> _sender;
    public EstablishmentController _establishmentController;

    public EstablishmentControllerTests()
    {
        _sender = new Mock<ISender>();
        _establishmentController = new EstablishmentController(_sender.Object);
    }

    [Fact]
    public async Task GetAll_Return_200_Ok()
    {
        var response = CreateEstablishmentQueryResponse().Generate(5);
        _sender.Setup(_ => _.Send(It.IsAny<EstablishmentQuery>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(response);

        var result = await _establishmentController.GetAll();

        result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetAll_Return_404_Not_Found()
    {
        _sender.Setup(_ => _.Send(It.IsAny<EstablishmentQuery>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(Enumerable.Empty<EstablishmentQueryResponse>);

        var result = await _establishmentController.GetAll();

        result.Should().BeOfType<NotFoundResult>().Which.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetAll_Return_500_Internal_Server_Error()
    {
        _sender.Setup(_ => _.Send(It.IsAny<EstablishmentQuery>(), It.IsAny<CancellationToken>()))
               .Throws(new Exception());

        var result = await _establishmentController.GetAll();

        result.Should().BeOfType<StatusCodeResult>().Which.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
    }

    private static Faker<EstablishmentQueryResponse> CreateEstablishmentQueryResponse()
    {
        return new Faker<EstablishmentQueryResponse>()
            .WithRecord()
            .RuleFor(_ => _.Name, _ => _.Company.CompanyName())
            .RuleFor(_ => _.Owner, _ => new OwnerQueryResponse(_.Person.Cpf(), _.Person.FullName))
            .RuleFor(_ => _.Transaction, _ => new List<TransactionQueryResponse>
            {
                new TransactionQueryResponse(_.Random.Int(0, 9).ToString(), _.Date.Past(5), _.Random.Decimal(0, 2000), _.Name.Random.ToString(), _.Date.Timespan())
            });
            
    }
}