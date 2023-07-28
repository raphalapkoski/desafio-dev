using MediatR;

namespace DesafioDev.Application.Abstractions.Command;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
                 where TCommand : ICommand<TResponse>
{
}
