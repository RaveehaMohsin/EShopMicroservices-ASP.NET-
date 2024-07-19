
using MediatR;

namespace buildingblocks.CQRS
{
    public interface IQuery <out tresponse> : IRequest<tresponse>
        where tresponse : notnull
    {
    }
}
