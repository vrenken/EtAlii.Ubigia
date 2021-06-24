// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Workflow
{
    // ReSharper disable once UnusedTypeParameter
    public abstract class QueryBase<TQueryHandler, TResult> : IQuery<TResult>
        where TQueryHandler : class, IQueryHandler<TResult>
    {
        //IQueryHandler<TResult> IQuery<TResult>.GetHandler(Container container)
        //[
        //    return container.GetInstance<TQueryHandler>()
        //]
    }
}
