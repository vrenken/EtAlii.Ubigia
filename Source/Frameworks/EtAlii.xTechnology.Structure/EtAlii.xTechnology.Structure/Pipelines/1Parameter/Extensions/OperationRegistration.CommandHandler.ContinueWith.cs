// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Pipelines
{
    public static partial class OperationRegistrationCommandHandlerExtensions
    {
        public static IOperationRegistration<TPipelineIn, TOut, TOut> ContinueWith<TPipelineIn, TIn, TOut, TCommand>(
            this IOperationRegistration<TPipelineIn, TIn, TOut> registration, ICommandHandler<TCommand, TOut> commandHandler)
            where TCommand : ICommand<TOut>
        {
            var pipelineProvider = (IPipelineProvider<TPipelineIn>)registration;
            var pipeline = pipelineProvider.GetPipeline();
            var factoryProvider = (IOperationRegistrationFactoryProvider<TPipelineIn>)pipeline;
            var factory = factoryProvider.GetFactory<TOut, TOut>();
            var newRegistration = factory.Create(input =>
            {
                var command = commandHandler.Create(input);
                commandHandler.Handle(command);
                return input;
            });

            var operationChain = (IOperationChain<TIn, TOut>)registration;
            var nextOperation = (IOperationChain<TOut, TOut>)newRegistration;
            operationChain.Register(nextOperation.Process);

            return newRegistration;
        }


        public static IOperationRegistration<TPipelineIn, TPipelineOut, TOut, TOut> ContinueWith<TPipelineIn, TPipelineOut, TIn, TOut, TCommand>(
            this IOperationRegistration<TPipelineIn, TPipelineOut, TIn, TOut> registration, ICommandHandler<TCommand, TOut> commandHandler)
            where TCommand : ICommand<TOut>
        {
            var pipelineProvider = (IPipelineProvider<TPipelineIn, TPipelineOut>)registration;
            var pipeline = pipelineProvider.GetPipeline();
            var factoryProvider = (IOperationRegistrationFactoryProvider<TPipelineIn, TPipelineOut>)pipeline;
            var factory = factoryProvider.GetFactory<TOut, TOut>();
            var newRegistration = factory.Create(input =>
            {
                var command = commandHandler.Create(input);
                commandHandler.Handle(command);
                return input;
            });

            var operationChain = (IOperationChain<TPipelineOut, TIn, TOut>)registration;
            var nextOperation = (IOperationChain<TPipelineOut, TOut, TOut>)newRegistration;
            operationChain.Register(nextOperation.Process);

            return newRegistration;
        }
    }
}