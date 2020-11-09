﻿namespace EtAlii.xTechnology.Structure.Workflow
{
    using System.Linq;

    public interface IQueryProcessor
    {
        IQueryable<TResult> Process<TResult>(IQuery<TResult> query, IQueryHandler<TResult> handler);
    }
}