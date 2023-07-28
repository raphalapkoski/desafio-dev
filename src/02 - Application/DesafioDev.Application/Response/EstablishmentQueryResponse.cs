namespace DesafioDev.Application.Response;

public sealed record EstablishmentQueryResponse(string Name, OwnerQueryResponse Owner, IEnumerable<TransactionQueryResponse> Transaction, decimal TotalEntry, decimal TotalExit, decimal TotalBalance);

public sealed record OwnerQueryResponse(string Cpf, string Name);

public sealed record TransactionQueryResponse(string Type, DateTime Date, decimal Value, string Card, TimeSpan Hour);

