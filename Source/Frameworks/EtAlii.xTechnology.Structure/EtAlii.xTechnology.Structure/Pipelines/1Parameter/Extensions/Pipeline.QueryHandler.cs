// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Pipelines
{
    public static partial class PipelineQueryHandlerExtensions
    {
        public static IOperationRegistration<TPipelineIn, TPipelineIn, TOut> StartWith<TPipelineIn, TOut, TQuery>(
            this IPipeline<TPipelineIn> pipeline, IQueryHandler<TQuery, TPipelineIn, TOut> queryHandler)
            where TQuery : IQuery<TPipelineIn>
        {
            var factoryProvider = (IOperationRegistrationFactoryProvider<TPipelineIn>)pipeline;
            var factory = factoryProvider.GetFactory<TPipelineIn, TOut>();
            var registration = factory.Create(input =>
            {
                var query = queryHandler.Create(input);
                return queryHandler.Handle(query);
            });

            var operationChain = (IOperationChain<TPipelineIn, TPipelineIn>)pipeline;
            var nextOperation = (IOperationChain<TPipelineIn, TOut>)registration;
            operationChain.Register(nextOperation.Process);

            return registration;
        }

        public static IOperationRegistration<TPipelineIn, TPipelineOut, TPipelineIn, TOut> StartWith<TPipelineIn, TPipelineOut, TOut, TQuery>(
            this IPipeline<TPipelineIn, TPipelineOut> pipeline, IQueryHandler<TQuery, TPipelineIn, TOut> queryHandler)
            where TQuery : IQuery<TPipelineIn>
        {
            var factoryProvider = (IOperationRegistrationFactoryProvider<TPipelineIn, TPipelineOut>)pipeline;
            var factory = factoryProvider.GetFactory<TPipelineIn, TOut>();
            var registration = factory.Create(input =>
            {
                var query = queryHandler.Create(input);
                return queryHandler.Handle(query);
            });

            var operationChain = (IOperationChain<TPipelineOut, TPipelineIn, TPipelineIn>)pipeline;
            var nextOperation = (IOperationChain<TPipelineOut, TPipelineIn, TOut>) registration;
            operationChain.Register(nextOperation.Process);

            return registration;
        }
    }
}