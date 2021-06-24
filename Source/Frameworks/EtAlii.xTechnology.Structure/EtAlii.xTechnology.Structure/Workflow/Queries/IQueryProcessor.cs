// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Workflow
{
    using System.Linq;

    public interface IQueryProcessor
    {
        IQueryable<TResult> Process<TResult>(IQuery<TResult> query, IQueryHandler<TResult> handler);
    }
}