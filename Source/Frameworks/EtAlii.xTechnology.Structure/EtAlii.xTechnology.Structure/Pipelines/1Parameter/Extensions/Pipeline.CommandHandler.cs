// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Pipelines
{
    public static class PipelineCommandHandlerExtensions
    {
        public static IOperationRegistration<TPipelineIn, TPipelineIn, TPipelineIn> StartWith<TPipelineIn, TCommand>(
            this IPipeline<TPipelineIn> pipeline, ICommandHandler<TCommand, TPipelineIn> commandHandler)
            where TCommand : ICommand<TPipelineIn>
        {
            var factoryProvider = (IOperationRegistrationFactoryProvider<TPipelineIn>)pipeline;
            var factory = factoryProvider.GetFactory<TPipelineIn, TPipelineIn>();
            var registration = factory.Create(input =>
            {
                var query = commandHandler.Create(input);
                commandHandler.Handle(query);
                return input;
            });

            var operationChain = (IOperationChain<TPipelineIn, TPipelineIn>)pipeline;
            var nextOperation = (IOperationChain<TPipelineIn, TPipelineIn>)registration;
            operationChain.Register(nextOperation.Process);

            return registration;
        }

        public static IOperationRegistration<TPipelineIn, TPipelineOut, TPipelineIn, TPipelineIn> StartWith<TPipelineIn, TPipelineOut, TCommand>(
            this IPipeline<TPipelineIn, TPipelineOut> pipeline, ICommandHandler<TCommand, TPipelineIn> commandHandler)
            where TCommand : ICommand<TPipelineIn>
        {
            var factoryProvider = (IOperationRegistrationFactoryProvider<TPipelineIn, TPipelineOut>)pipeline;
            var factory = factoryProvider.GetFactory<TPipelineIn, TPipelineIn>();
            var registration = factory.Create(input =>
            {
                var query = commandHandler.Create(input);
                commandHandler.Handle(query);
                return input;
            });

            var operationChain = (IOperationChain<TPipelineOut, TPipelineIn, TPipelineIn>)pipeline;
            var nextOperation = (IOperationChain<TPipelineOut, TPipelineIn, TPipelineIn>)registration;
            operationChain.Register(nextOperation.Process);

            return registration;
        }
    }
}