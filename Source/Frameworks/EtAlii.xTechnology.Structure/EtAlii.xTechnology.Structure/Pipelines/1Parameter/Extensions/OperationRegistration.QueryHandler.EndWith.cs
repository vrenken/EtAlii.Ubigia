// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Pipelines
{
    public static partial class OperationRegistrationQueryHandlerExtensions
    {
        public static IPipeline<TPipelineIn, TPipelineOut> EndWith<TPipelineIn, TPipelineOut, TIn, TOut, TQuery>(
            this IOperationRegistration<TPipelineIn, TPipelineOut, TIn, TOut> registration, IQueryHandler<TQuery, TOut, TPipelineOut> queryHandler)
            where TQuery : IQuery<TOut>
        {
            var pipelineProvider = (IPipelineProvider<TPipelineIn, TPipelineOut>)registration;
            var pipeline = pipelineProvider.GetPipeline();
            var factoryProvider = (IOperationRegistrationFactoryProvider<TPipelineIn, TPipelineOut>)pipeline;
            var factory = factoryProvider.GetFactory<TOut, TPipelineOut>();
            var newRegistration = factory.Create(input =>
            {
                var query = queryHandler.Create(input);
                return queryHandler.Handle(query);
            });

            var operationChain = (IOperationChain<TPipelineOut, TIn, TOut>)registration;
            var nextOperation = (IOperationChain<TPipelineOut, TOut, TPipelineOut>)newRegistration;
            operationChain.Register(nextOperation.Process);
            nextOperation.Register(v => v);

            return pipeline;
        }
    }
}