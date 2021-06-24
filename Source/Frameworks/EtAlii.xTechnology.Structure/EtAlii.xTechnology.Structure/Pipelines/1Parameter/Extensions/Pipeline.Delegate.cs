// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Pipelines
{
    public static class PipelineDelegateExtensions
    {
        public static IOperationRegistration<TPipelineIn, TPipelineIn, TOut> StartWith<TPipelineIn, TOut>(
            this IPipeline<TPipelineIn> pipeline, Operation<TPipelineIn, TOut> operation)
        {
            var factoryProvider = (IOperationRegistrationFactoryProvider<TPipelineIn>)pipeline;
            var factory = factoryProvider.GetFactory<TPipelineIn, TOut>();
            var registration = factory.Create(input => operation(input));

            var operationChain = (IOperationChain<TPipelineIn, TPipelineIn>)pipeline;
            var nextOperation = (IOperationChain<TPipelineIn, TOut>)registration;
            operationChain.Register(nextOperation.Process);
            
            return registration;
        }

        public static IOperationRegistration<TPipelineIn, TPipelineOut, TPipelineIn, TOut> StartWith<TPipelineIn, TPipelineOut, TOut>(
            this IPipeline<TPipelineIn, TPipelineOut> pipeline, Operation<TPipelineIn, TOut> operation)
        {
            var factoryProvider = (IOperationRegistrationFactoryProvider<TPipelineIn, TPipelineOut>)pipeline;
            var factory = factoryProvider.GetFactory<TPipelineIn, TOut>();
            var registration = factory.Create(input => operation(input));

            var operationChain = (IOperationChain<TPipelineOut, TPipelineIn, TPipelineIn>)pipeline;
            var nextOperation = (IOperationChain<TPipelineOut, TPipelineIn, TOut>)registration;
            operationChain.Register(nextOperation.Process);
            
            return registration;
        }
    }
}