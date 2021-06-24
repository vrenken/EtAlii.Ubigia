// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Workflow
{
    using System.Linq;

    public class QueryProcessor : IQueryProcessor
    {
        public IQueryable<TResult> Process<TResult>(IQuery<TResult> query, IQueryHandler<TResult> handler)
        {
            //var handler = query.GetHandler(_container)
            return handler.Handle(query);
        }
    }
}
