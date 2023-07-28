using DesafioDev.Application.Abstractions.Query;
using DesafioDev.Application.Response;

namespace DesafioDev.Application.Features.Establishment
{
    public sealed record EstablishmentQuery() : IQuery<IEnumerable<EstablishmentQueryResponse>>;
}
