using MediatR;

namespace DesafioDev.Application.Abstractions.Command;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
