// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Pipelines
{
    public static partial class OperationRegistrationCommandHandlerExtensions
    {
        public static IPipeline<TPipelineIn> EndWith<TPipelineIn, TIn, TOut, TCommand>(
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
                return default(TOut);
            });

            var operationChain = (IOperationChain<TIn, TOut>)registration;
            var nextOperation = (IOperationChain<TOut, TOut>)newRegistration;
            operationChain.Register(nextOperation.Process);
            nextOperation.Register(_ => { });

            return pipeline;
        }
    }
}