using MediatR;

namespace DesafioDev.Application.Abstractions.Query;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
