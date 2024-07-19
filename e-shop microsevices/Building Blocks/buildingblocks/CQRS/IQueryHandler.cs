using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buildingblocks.CQRS
{

    public interface IQueryHandler<in TQuery> : IRequestHandler<TQuery, Unit>
    where TQuery : IQuery<Unit>
    {
    }
    public interface IQueryHandler<in TQuery, Tresponse> : IRequestHandler<TQuery, Tresponse>
        where TQuery : IQuery<Tresponse>
        where Tresponse : notnull
    {
    }
}
