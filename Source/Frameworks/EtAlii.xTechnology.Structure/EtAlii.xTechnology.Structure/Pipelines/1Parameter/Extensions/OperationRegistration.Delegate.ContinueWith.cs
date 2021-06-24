// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Pipelines
{
    public static partial class OperationRegistrationDelegateExtensions
    {

        public static IOperationRegistration<TPipelineIn, TOut, TOut2> ContinueWith<TPipelineIn, TIn, TOut, TOut2>(
            this IOperationRegistration<TPipelineIn, TIn, TOut> registration, Operation<TOut, TOut2> operation)
        {
            var pipelineProvider = (IPipelineProvider<TPipelineIn>)registration;
            var pipeline = pipelineProvider.GetPipeline();
            var factoryProvider = (IOperationRegistrationFactoryProvider<TPipelineIn>)pipeline;
            var factory = factoryProvider.GetFactory<TOut, TOut2>();
            var newRegistration = factory.Create(input => operation(input));

            var operationChain = (IOperationChain<TIn, TOut>)registration;
            var nextOperation = (IOperationChain<TOut, TOut2>)newRegistration;
            operationChain.Register(nextOperation.Process);
            
            return newRegistration;
        }


        public static IOperationRegistration<TPipelineIn, TOut, TOut> ContinueWith<TPipelineIn, TIn, TOut>(
            this IOperationRegistration<TPipelineIn, TIn, TOut> registration, Operation<TOut> operation)
        {
            var pipelineProvider = (IPipelineProvider<TPipelineIn>)registration;
            var pipeline = pipelineProvider.GetPipeline();
            var factoryProvider = (IOperationRegistrationFactoryProvider<TPipelineIn>)pipeline;
            var factory = factoryProvider.GetFactory<TOut, TOut>();
            var newRegistration = factory.Create(input => 
            { 
                operation(input);
                return input;
            });

            var operationChain = (IOperationChain<TIn, TOut>)registration;
            // ReSharper disable once SuspiciousTypeConversion.Global
            var nextOperation = (IOperationChain<TOut, TIn>)newRegistration;
            operationChain.Register(nextOperation.Process);

            return newRegistration;
        }

        public static IOperationRegistration<TPipelineIn, TPipelineOut, TOut, TOut2> ContinueWith<TPipelineIn, TPipelineOut, TIn, TOut, TOut2>(
            this IOperationRegistration<TPipelineIn, TPipelineOut, TIn, TOut> registration, Operation<TOut, TOut2> operation)
        {
            var pipelineProvider = (IPipelineProvider<TPipelineIn, TPipelineOut>)registration;
            var pipeline = pipelineProvider.GetPipeline();
            var factoryProvider = (IOperationRegistrationFactoryProvider<TPipelineIn, TPipelineOut>)pipeline;
            var factory = factoryProvider.GetFactory<TOut, TOut2>();
            var newRegistration = factory.Create(input => operation(input));

            var operationChain = (IOperationChain<TPipelineOut, TIn, TOut>)registration;
            var nextOperation = (IOperationChain<TPipelineOut, TOut, TOut2>)newRegistration;
            operationChain.Register(nextOperation.Process);

            return newRegistration;
        }

        
        public static IOperationRegistration<TPipelineIn, TPipelineOut, TOut, TOut> ContinueWith<TPipelineIn, TPipelineOut, TIn, TOut>(
            this IOperationRegistration<TPipelineIn, TPipelineOut, TIn, TOut> registration, Operation<TOut> operation)
        {
            var pipelineProvider = (IPipelineProvider<TPipelineIn, TPipelineOut>)registration;
            var pipeline = pipelineProvider.GetPipeline();
            var factoryProvider = (IOperationRegistrationFactoryProvider<TPipelineIn, TPipelineOut>)pipeline;
            var factory = factoryProvider.GetFactory<TOut, TOut>();
            var newRegistration = factory.Create(input => 
            { 
                operation(input);
                return input;
            });

            var operationChain = (IOperationChain<TPipelineOut, TIn, TOut>)registration;
            var nextOperation = (IOperationChain<TPipelineOut, TOut, TOut>)newRegistration;
            operationChain.Register(nextOperation.Process);

            return newRegistration;
        }

    }
}