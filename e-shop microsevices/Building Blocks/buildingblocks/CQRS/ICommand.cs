
using MediatR;

namespace buildingblocks.CQRS
{
    public interface ICommand : ICommand<Unit>  //for void type constructor ...doesnt product response
    {

    }
    public interface ICommand<out tresponse> : IRequest<tresponse>  //for other..produces a response
    {
    }
}
