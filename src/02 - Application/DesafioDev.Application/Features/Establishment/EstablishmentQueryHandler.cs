using DesafioDev.Application.Abstractions.Query;
using DesafioDev.Application.Response;
using DesafioDev.Domain.Repositories;

namespace DesafioDev.Application.Features.Establishment;

public class EstablishmentQueryHandler : IQueryHandler<EstablishmentQuery, IEnumerable<EstablishmentQueryResponse>>
{
    readonly IUnitOfWork _unitOfWork;

    public EstablishmentQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<EstablishmentQueryResponse>> Handle(EstablishmentQuery request, CancellationToken cancellationToken)
    {
        var list = await _unitOfWork.EstablishmentRepository.GetAllAsync();

        return list.Select(_ => new EstablishmentQueryResponse(_.Name, 
                                new OwnerQueryResponse(_.Owner.Cpf, _.Owner.Name), 
                                _.Transactions.Select(_ => new TransactionQueryResponse(_.Type.ToString(), _.Date, _.Value, _.Card, _.Hour)),
                                _.CalculateTotalEntryValue(),
                                _.CalculateTotalExitValue(),
                                _.CalculateTotalBalance())) ?? new List<EstablishmentQueryResponse>();
    }
}

