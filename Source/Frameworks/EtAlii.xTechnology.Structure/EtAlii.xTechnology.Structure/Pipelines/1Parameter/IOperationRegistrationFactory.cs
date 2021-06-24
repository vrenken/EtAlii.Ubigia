// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Pipelines
{
    using System;

    public interface IOperationRegistrationFactory<in TPipelineIn, out TPipelineOut, TIn, TOut>
    {
        IOperationRegistration<TPipelineIn, TPipelineOut, TIn, TOut> Create(Func<TIn, TOut> operation);
    }

    public interface IOperationRegistrationFactory<in TPipelineIn, TIn, TOut>
    {
        IOperationRegistration<TPipelineIn, TIn, TOut> Create(Func<TIn, TOut> operation);
    }
}