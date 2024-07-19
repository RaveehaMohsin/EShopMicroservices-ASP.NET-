
using MediatR;

namespace buildingblocks.CQRS
{
    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Unit>
        where TCommand : ICommand<Unit>
    {
    }
    public interface ICommandHandler<in TCommand , Tresponse> : IRequestHandler<TCommand, Tresponse> 
        where TCommand : ICommand<Tresponse>
        where Tresponse : notnull
    {
    }
}
