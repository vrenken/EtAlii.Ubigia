// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Workflow
{
    using System.Linq;

    public abstract class QueryHandlerBase<TQuery, TResult> : IQueryHandler<TResult>
        where TQuery : IQuery<TResult>
    {
        protected internal abstract IQueryable<TResult> Handle(TQuery query);

        public IQueryable<TResult> Handle(IQuery<TResult> query)
        {
            return Handle((TQuery)query);
        }
    }
}
