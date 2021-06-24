// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Pipelines
{
    using System;

    public class DefaultOperationRegistrationFactory<TPipelineIn, TIn, TOut> : IOperationRegistrationFactory<TPipelineIn, TIn, TOut>
    {
        private readonly IPipeline<TPipelineIn> _pipeline;

        public DefaultOperationRegistrationFactory(IPipeline<TPipelineIn> pipeline)
        {
            _pipeline = pipeline;
        }

        public IOperationRegistration<TPipelineIn, TIn, TOut> Create(Func<TIn, TOut> operation)
        {
            return new DefaultOperationRegistration<TPipelineIn, TIn, TOut>(_pipeline, operation);
        }
    }

    public class DefaultOperationRegistrationFactory<TPipelineIn, TPipelineOut, TIn, TOut> : IOperationRegistrationFactory<TPipelineIn, TPipelineOut, TIn, TOut>
    {
        private readonly IPipeline<TPipelineIn, TPipelineOut> _pipeline;

        public DefaultOperationRegistrationFactory(IPipeline<TPipelineIn, TPipelineOut> pipeline)
        {
            _pipeline = pipeline;
        }

        public IOperationRegistration<TPipelineIn, TPipelineOut, TIn, TOut> Create(Func<TIn, TOut> operation)
        {
            return new DefaultOperationRegistration<TPipelineIn, TPipelineOut, TIn, TOut>(_pipeline, operation);
        }
    }
}